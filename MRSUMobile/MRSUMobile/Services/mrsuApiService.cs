using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MRSUMobile.Models;
using System;
using System.Text.Json;
using MRSUMobile.Models.User;
using System.Net.Http.Headers;
using System.Threading;
using MonkeyCache.FileStore;

namespace MRSUMobile.Services
{
	public class MrsuApiService : IMrsuApiService
	{
		private Token BEAVER_TOKEN;

		private const string BASE_URL = @"https://papi.mrsu.ru/";
		private const string BASE_AUTORIZATION_URL = @"https://p.mrsu.ru/OAuth/";

		private const string CLIENT_ID_AUTORIZATION = "8";
		private const string CLIENT_SECRET_AUTORIZATION = "qweasd";

		private HttpClient MrsuApi = new HttpClient() { BaseAddress = new Uri(BASE_URL) };
		private HttpClient MrsuAutorization = new HttpClient() { BaseAddress = new Uri(BASE_AUTORIZATION_URL) };

		public void SetToken(Token bearer)
		{
			BEAVER_TOKEN = bearer;
			MrsuApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BEAVER_TOKEN.access_token);
		}
		public bool IsAutorized()
		{
			if (BEAVER_TOKEN is null)
			{
				return false;
			}

			return BEAVER_TOKEN.IsExpired();
		}

		public async Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default)
		{
			var tokenResponse = await MrsuAutorization.SendAsync(new HttpRequestMessage(HttpMethod.Post, @"Token")
			{
				Content = new FormUrlEncodedContent(new Dictionary<string, string>()
				{
					{ "grant_type", "password" },
					{ "client_id", CLIENT_ID_AUTORIZATION },
					{ "client_secret", CLIENT_SECRET_AUTORIZATION },
					{ "username", username },
					{ "password", password }
				})
			}, cancellationToken);

			if (!tokenResponse.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Can not create bearer token. Throwed with '{tokenResponse.StatusCode}' status code.");
			}

			return await JsonSerializer.DeserializeAsync<Token>
			(
				await tokenResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
		}
		public async Task<Token> Refresh(Token refresh_token, CancellationToken cancellationToken = default)
		{
			var tokenResponse = await MrsuAutorization.SendAsync(new HttpRequestMessage(HttpMethod.Post, @"Token")
			{
				Content = new FormUrlEncodedContent(new Dictionary<string, string>()
				{
					{ "grant_type", "refresh_token" },
					{ "client_id", CLIENT_ID_AUTORIZATION },
					{ "client_secret", CLIENT_SECRET_AUTORIZATION },
					{ "refresh_token", refresh_token.refresh_token }
				})
			}, cancellationToken);

			if (!tokenResponse.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Can not refresh bearer token. Throwed with '{tokenResponse.StatusCode}' status code.");
			}

			return await JsonSerializer.DeserializeAsync<Token>
			(
				await tokenResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
		}
		
		public async Task<User> GetUser(CancellationToken cancellationToken = default)
		{
			var cachekey = $"cahce_GetUser";

			if(Barrel.Current.Exists(cachekey))
			{
				return Barrel.Current.Get<User>(cachekey);
			}

			if (!IsAutorized())
			{
				SetToken(await Refresh(BEAVER_TOKEN));
			}

			var userResponse = await MrsuApi.SendAsync(new HttpRequestMessage(HttpMethod.Get, @"v1/User"), cancellationToken);

			if (!userResponse.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Can not get user info. Throwed with '{userResponse.StatusCode}' status code.");
			}

			var myProfile = await JsonSerializer.DeserializeAsync<User>
			(
				await userResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);

			Barrel.Current.Add(cachekey, myProfile, TimeSpan.FromHours(2));

			return myProfile;
		}

		public async Task<Diary> GetDiary(DateTime date, CancellationToken cancellationToken = default)
		{
			var cachekey = $"cache_GetDiary--{date.ToShortDateString()}";

			if (Barrel.Current.Exists(cachekey))
			{
				return Barrel.Current.Get<Diary>(cachekey);
			}

			if (!IsAutorized())
			{
				SetToken(await Refresh(BEAVER_TOKEN));
			}

			var diaryResponse = await MrsuApi.SendAsync(new HttpRequestMessage(HttpMethod.Get, @"v1/StudentTimeTable")
			{
				Content = new FormUrlEncodedContent(new Dictionary<string, string>()
				{
					{ "date", date.ToShortDateString() }
				})
			}, cancellationToken);

			if (!diaryResponse.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Can not get diary. Throwed with '{diaryResponse.StatusCode}' status code.");
			}

			var myDiary = await JsonSerializer.DeserializeAsync<Diary>
			(
				await diaryResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);

			Barrel.Current.Add(cachekey, myDiary, TimeSpan.FromHours(1));

			return myDiary;
		}
		public async Task<StudentAttendance> SentCode(string code, CancellationToken cancellationToken = default)
		{
			if (!IsAutorized())
			{
				SetToken(await Refresh(BEAVER_TOKEN));
			}

			var codeResponse = await MrsuApi.SendAsync(new HttpRequestMessage(HttpMethod.Post, @"v1/StudentTimeTable")
			{
				Content = new FormUrlEncodedContent(new Dictionary<string, string>()
				{
					{ "code", code }
				})
			}, cancellationToken);

			if (!codeResponse.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Can not get attendance server response. Throwed with '{codeResponse.StatusCode}' status code.");
			}

			return await JsonSerializer.DeserializeAsync<StudentAttendance>
			(
				await codeResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
		}
	}
}
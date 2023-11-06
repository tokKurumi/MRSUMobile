using Microsoft.Extensions.Configuration;
using MonkeyCache.FileStore;
using MRSUMobile.Entities;
using MRSUMobile.Helpers;
using MRSUMobile.MVVM.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web.Http;

namespace MRSUMobile.Services
{
	public interface IMrsuApiService
	{
		Token BearerToken { get; }

		Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default);
		Task<User> GetMyProfile(CancellationToken cancellationToken = default);
		Task<StudentTimeTable> GetTimeTable(DateTime date, CancellationToken cancellationToken = default);
		bool IsAutorized();
		Task<HttpStatusCode> Ping();
		Task<Token> RefreshSession(Token refreshToken, CancellationToken cancellationToken = default);
		Task<StudentAttendanceCode> SendAttendanceCode(string code, CancellationToken cancellationToken = default);
		void SetToken(Token bearer);
	}

	public class MrsuApiService : IMrsuApiService
	{
		Preferenses preferenses;

		const string BASE_URL = @"https://papi.mrsu.ru/";
		const string BASE_AUTORIZATION_URL = @"https://p.mrsu.ru/OAuth/";
		const string CLIENT_ID_AUTORIZATION = "8";
		const string CLIENT_SECRET_AUTORIZATION = "qweasd";

		HttpClient MrsuApi = new HttpClient() { BaseAddress = new Uri(BASE_URL) };
		HttpClient MrsuAutorizationApi = new HttpClient() { BaseAddress = new Uri(BASE_AUTORIZATION_URL) };

		public MrsuApiService(IConfiguration configuration)
		{
			preferenses = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
		}

		public async Task<HttpStatusCode> Ping()
		{
			var pingResponse = await MrsuApi.GetAsync(@"v1/Ping");

			return pingResponse.StatusCode;
		}

		public Token BearerToken { get; private set; }
		public void SetToken(Token bearer)
		{
			BearerToken = bearer;
			MrsuApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken.AccessToken);
		}
		public bool IsAutorized()
		{
			if (BearerToken is null)
			{
				return false;
			}

			return BearerToken.IsExpired();
		}

		public async Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default)
		{
			var tokenResponse = await MrsuAutorizationApi.PostAsync(@"Token", new FormUrlEncodedContent(new Dictionary<string, string>()
			{
				{ "grant_type", "password" },
				{ "client_id", CLIENT_ID_AUTORIZATION },
				{ "client_secret", CLIENT_SECRET_AUTORIZATION },
				{ "username", username },
				{ "password", password }
			}), cancellationToken);

			if (!tokenResponse.IsSuccessStatusCode)
			{
				throw new HttpResponseException(tokenResponse);
			}

			var myToken = await JsonSerializer.DeserializeAsync<Token>
			(
				await tokenResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
			PreferenceStorageProvider.Set(preferenses.Token, myToken);

			return myToken;
		}
		public async Task<Token> RefreshSession(Token refreshToken, CancellationToken cancellationToken = default)
		{
			var tokenResponse = await MrsuAutorizationApi.PostAsync(@"Token", new FormUrlEncodedContent(new Dictionary<string, string>()
			{
				{ "grant_type", "refresh_token" },
				{ "client_id", CLIENT_ID_AUTORIZATION },
				{ "client_secret", CLIENT_SECRET_AUTORIZATION },
				{ "refresh_token", refreshToken.RefreshToken }
			}), cancellationToken);

			if (!tokenResponse.IsSuccessStatusCode)
			{
				throw new HttpResponseException(tokenResponse);
			}

			var myToken = await JsonSerializer.DeserializeAsync<Token>
			(
				await tokenResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
			PreferenceStorageProvider.Set(preferenses.Token, myToken);

			return myToken;
		}

		public async Task<User> GetMyProfile(CancellationToken cancellationToken = default)
		{
			var cachekey = $"cahce_GetUser";

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				return Barrel.Current.Get<User>(cachekey);
			}

			if (!IsAutorized())
			{
				SetToken(await RefreshSession(BearerToken));
			}

			var userResponse = await MrsuApi.GetAsync(@"v1/User", cancellationToken);

			if (!userResponse.IsSuccessStatusCode)
			{
				throw new HttpResponseException(userResponse);
			}

			var myProfile = await JsonSerializer.DeserializeAsync<User>
			(
				await userResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);

			Barrel.Current.Add(cachekey, myProfile, TimeSpan.FromDays(7));

			return myProfile;
		}

		public async Task<StudentTimeTable> GetTimeTable(DateTime date, CancellationToken cancellationToken = default)
		{
			var cachekey = $"cache_GetDiary--{date.ToShortDateString()}";

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				return Barrel.Current.Get<StudentTimeTable>(cachekey);
			}

			if (!IsAutorized())
			{
				SetToken(await RefreshSession(BearerToken));
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
				throw new HttpResponseException(diaryResponse);
			}

			var myTimeTable = await JsonSerializer.DeserializeAsync<StudentTimeTable>
			(
				await diaryResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);

			Barrel.Current.Add(cachekey, myTimeTable, TimeSpan.FromDays(1));

			return myTimeTable;
		}

		public async Task<StudentAttendanceCode> SendAttendanceCode(string code, CancellationToken cancellationToken = default)
		{
			if (!IsAutorized())
			{
				SetToken(await RefreshSession(BearerToken));
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
				throw new HttpResponseException(codeResponse);
			}

			return await JsonSerializer.DeserializeAsync<StudentAttendanceCode>
			(
				await codeResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
		}
	}
}
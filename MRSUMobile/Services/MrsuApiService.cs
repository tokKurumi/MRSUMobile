using MonkeyCache.FileStore;
using MRSUMobile.Entities;
using MRSUMobile.MVVM.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MRSUMobile.Services
{
	public class MrsuApiService : IMrsuApiService
	{
		private const string BASE_URL = @"https://papi.mrsu.ru/";
		private const string BASE_AUTORIZATION_URL = @"https://p.mrsu.ru/OAuth/";
		private const string CLIENT_ID_AUTORIZATION = "8";
		private const string CLIENT_SECRET_AUTORIZATION = "qweasd";

		private HttpClient MrsuApi = new HttpClient() { BaseAddress = new Uri(BASE_URL) };
		private HttpClient MrsuAutorizationApi = new HttpClient() { BaseAddress = new Uri(BASE_AUTORIZATION_URL) };

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
			var cachekey = $"cahce_Autorize--{username}--{password}";

			if (Barrel.Current.Exists(cachekey))
			{
				var cacheToken = Barrel.Current.Get<Token>(cachekey); // trying to get existing token from cache memory
				var newToken = await RefreshSession(cacheToken); // and then refresh it, so it will become new

				if (newToken is not null) // if token was succefully refreshed that means all done good, no need to ask api to generate new one
				{
					return newToken;
				} // but if we got null that means mem. cached token was already expired - ask api for a new one
			}

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
				return null;
			}

			var myToken = await JsonSerializer.DeserializeAsync<Token>
			(
				await tokenResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);

			Barrel.Current.Add(cachekey, myToken, TimeSpan.FromHours(2));

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
				return null;
			}

			return await JsonSerializer.DeserializeAsync<Token>
			(
				await tokenResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
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
				return null;
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
				return null;
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
			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				return null;
			}

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
				return null;
			}

			return await JsonSerializer.DeserializeAsync<StudentAttendanceCode>
			(
				await codeResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
		}
	}
}
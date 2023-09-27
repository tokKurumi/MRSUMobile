using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MRSUMobile.Models;
using System;
using System.Text.Json;
using MRSUMobile.Models.User;
using System.Net.Http.Headers;
using System.Threading;

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
		public async Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default)
		{
			var tokenResponse = await MrsuAutorization.SendAsync(new HttpRequestMessage(HttpMethod.Post, "Token")
			{
				Content = new FormUrlEncodedContent(new Dictionary<string, string>()
				{
					{ "grant_type", "password" },
					{ "client_id", CLIENT_ID_AUTORIZATION },
					{ "client_secret", CLIENT_SECRET_AUTORIZATION },
					{ "username", username },
					{ "password", password },
					{ "scope", "" }
				})
			}, cancellationToken);

			if (!tokenResponse.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Can not create bearer token. Throwed with {tokenResponse.StatusCode} status code.");
			}

			return await JsonSerializer.DeserializeAsync<Token>
			(
				await tokenResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
		}
		public async Task<User> GetUser(CancellationToken cancellationToken = default)
		{
			var userResponse = await MrsuApi.SendAsync(new HttpRequestMessage(HttpMethod.Get, @"/v1/User"), cancellationToken);

			if (!userResponse.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Can not create bearer token. Throwed with {userResponse.StatusCode} status code.");
			}

			return await JsonSerializer.DeserializeAsync<User>
			(
				await userResponse.Content.ReadAsStreamAsync(),
				cancellationToken: cancellationToken
			);
		}
		public bool IsAutorized()
		{
			if (BEAVER_TOKEN is null)
			{
				return false;
			}

			return BEAVER_TOKEN.IsExpired();
		}
	}
}
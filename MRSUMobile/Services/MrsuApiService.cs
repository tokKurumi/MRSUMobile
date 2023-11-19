namespace MRSUMobile.Services
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using MRSUMobile.Entities;
    using MRSUMobile.Exceptions;
    using MRSUMobile.MVVM.Model;

    public class MrsuApiService : IMrsuApiService
    {
        private const string _baseUrl = @"https://papi.mrsu.ru/";
        private const string _baseAutorizationUrl = @"https://p.mrsu.ru/OAuth/";
        private const string _clientIdAutorization = "8";
        private const string _clientSecretAutorization = "qweasd";

        public MrsuApiService()
        {
            MrsuApi = new HttpClient() { BaseAddress = new Uri(_baseUrl) };
            MrsuAutorizationApi = new HttpClient() { BaseAddress = new Uri(_baseAutorizationUrl) };
        }

        public Token BearerToken { get; private set; }

        private HttpClient MrsuApi { get; init; }

        private HttpClient MrsuAutorizationApi { get; init; }

        public async Task<HttpStatusCode> Ping()
        {
            var pingResponse = await MrsuApi.GetAsync(@"v1/Ping");

            return pingResponse.StatusCode;
        }

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

        public virtual async Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default)
        {
            var tokenResponse = await MrsuAutorizationApi.PostAsync(
                @"Token",
                new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "grant_type", "password" },
                    { "client_id", _clientIdAutorization },
                    { "client_secret", _clientSecretAutorization },
                    { "username", username },
                    { "password", password },
                }), cancellationToken);

            if (!tokenResponse.IsSuccessStatusCode)
            {
                throw new HttpResponseException(tokenResponse);
            }

            return await JsonSerializer.DeserializeAsync<Token>(
                await tokenResponse.Content.ReadAsStreamAsync(),
                cancellationToken: cancellationToken);
        }

        public virtual async Task<Token> RefreshSession(Token refreshToken, CancellationToken cancellationToken = default)
        {
            var tokenResponse = await MrsuAutorizationApi.PostAsync(
                @"Token",
                new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", _clientIdAutorization },
                    { "client_secret", _clientSecretAutorization },
                    { "refresh_token", refreshToken.RefreshToken },
                }), cancellationToken);

            if (!tokenResponse.IsSuccessStatusCode)
            {
                throw new HttpResponseException(tokenResponse);
            }

            var refreshedToken = await JsonSerializer.DeserializeAsync<Token>(
                await tokenResponse.Content.ReadAsStreamAsync(),
                cancellationToken: cancellationToken);

            SetToken(refreshedToken);

            return refreshedToken;
        }

        public virtual async Task<User> GetMyProfile(CancellationToken cancellationToken = default)
        {
            if (!IsAutorized())
            {
                await RefreshSession(BearerToken);
            }

            var userResponse = await MrsuApi.GetAsync(@"v1/User", cancellationToken);

            if (!userResponse.IsSuccessStatusCode)
            {
                throw new HttpResponseException(userResponse);
            }

            return await JsonSerializer.DeserializeAsync<User>(
                await userResponse.Content.ReadAsStreamAsync(),
                cancellationToken: cancellationToken);
        }

        public virtual async Task<StudentTimeTable> GetTimeTable(DateTime date, CancellationToken cancellationToken = default)
        {
            if (!IsAutorized())
            {
                await RefreshSession(BearerToken);
            }

            var diaryResponse = await MrsuApi.SendAsync(
                new HttpRequestMessage(HttpMethod.Get, @"v1/StudentTimeTable")
                {
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>()
                    {
                        { "date", date.ToShortDateString() },
                    }),
                }, cancellationToken);

            if (!diaryResponse.IsSuccessStatusCode)
            {
                throw new HttpResponseException(diaryResponse);
            }

            return await JsonSerializer.DeserializeAsync<StudentTimeTable>(
                await diaryResponse.Content.ReadAsStreamAsync(),
                cancellationToken: cancellationToken);
        }

        public async Task<StudentAttendanceCode> SendAttendanceCode(string code, CancellationToken cancellationToken = default)
        {
            if (!IsAutorized())
            {
                await RefreshSession(BearerToken);
            }

            var codeResponse = await MrsuApi.SendAsync(
                new HttpRequestMessage(HttpMethod.Post, @"v1/StudentTimeTable")
                {
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>()
                    {
                        { "code", code },
                    }),
                }, cancellationToken);

            if (!codeResponse.IsSuccessStatusCode)
            {
                throw new HttpResponseException(codeResponse);
            }

            return await JsonSerializer.DeserializeAsync<StudentAttendanceCode>(
                await codeResponse.Content.ReadAsStreamAsync(),
                cancellationToken: cancellationToken);
        }
    }
}
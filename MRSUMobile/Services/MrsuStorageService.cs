namespace MRSUMobile.Services
{
    using Microsoft.Extensions.Configuration;
    using MonkeyCache.FileStore;
    using MRSUMobile.Entities;
    using MRSUMobile.Exceptions;
    using MRSUMobile.MVVM.Model;

    public class MrsuStorageService : MrsuApiService
    {
        private Preferenses _preferenceConfig;

        public MrsuStorageService(IConfiguration configuration, IPreferenceStorageService preferenceStorageService)
        {
            _preferenceConfig = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
            Preference = preferenceStorageService;
        }

        public IPreferenceStorageService Preference { get; init; }

        public override async Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var token = await base.Autorize(username, password, cancellationToken);
                Preference.Set(_preferenceConfig.Token, token);

                return token;
            }
            catch (HttpResponseException)
            {
                throw;
            }
        }

        public override async Task<Token> RefreshSession(Token refreshToken, CancellationToken cancellationToken = default)
        {
            try
            {
                var newToken = await base.RefreshSession(refreshToken, cancellationToken);
                Preference.Set(_preferenceConfig.Token, newToken);

                return newToken;
            }
            catch (HttpResponseException)
            {
                throw;
            }
        }

        public override async Task<User> GetMyProfile(CancellationToken cancellationToken = default)
        {
            var cacheKey = $"GetMyProfile";
            var expireIn = TimeSpan.FromMinutes(10);

            if (Barrel.Current.Exists(cacheKey))
            {
                return Barrel.Current.Get<User>(cacheKey);
            }

            try
            {
                var myProfile = await base.GetMyProfile(cancellationToken);
                Barrel.Current.Add(cacheKey, myProfile, expireIn);

                return myProfile;
            }
            catch (HttpResponseException)
            {
                throw;
            }
        }
    }
}

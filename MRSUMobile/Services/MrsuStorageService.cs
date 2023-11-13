using Microsoft.Extensions.Configuration;
using MonkeyCache.FileStore;
using MRSUMobile.Entities;
using MRSUMobile.MVVM.Model;
using System.Web.Http;

namespace MRSUMobile.Services
{
    public class MrsuStorageService : MrsuApiService
    {
        Preferenses preferenceConfig;
        public IPreferenceStorageService Preference { get; init; }

        public MrsuStorageService(IConfiguration configuration, IPreferenceStorageService preferenceStorageService)
        {
            preferenceConfig = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
            Preference = preferenceStorageService;
        }

        public override async Task<Token> Autorize(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var token = await base.Autorize(username, password, cancellationToken);
                Preference.Set(preferenceConfig.Token, token);

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
                Preference.Set(preferenceConfig.Token, newToken);

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

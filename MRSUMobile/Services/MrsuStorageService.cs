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

            Diary = new Dictionary<DateOnly, List<StudentTimeTable>>();
            WorkingProgramm = new Dictionary<(int Year, int Period), StudentSemestr>();
        }

        public IPreferenceStorageService Preference { get; init; }

        public Dictionary<DateOnly, List<StudentTimeTable>> Diary { get; private set; }

        public Dictionary<(int Year, int Period), StudentSemestr> WorkingProgramm { get; private set; }

        public void Clear()
        {
            Preference.Clear();
            Barrel.Current.EmptyAll();
        }

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

        public override async Task<List<StudentTimeTable>> GetTimeTable(DateOnly date, CancellationToken cancellationToken = default)
        {
            if (Diary.ContainsKey(date))
            {
                return Diary[date];
            }

            var selectedDate = await base.GetTimeTable(date, cancellationToken);
            Diary.Add(date, selectedDate);

            return selectedDate;
        }

        public override async Task<StudentSemestr> GetSemestr(int year, int period, CancellationToken cancellationToken = default)
        {
            if (WorkingProgramm.ContainsKey((year, period)))
            {
                return WorkingProgramm[(year, period)];
            }

            var selectedSemestr = await base.GetSemestr(year, period, cancellationToken);
            WorkingProgramm.Add((year, period), selectedSemestr);

            return selectedSemestr;
        }
    }
}

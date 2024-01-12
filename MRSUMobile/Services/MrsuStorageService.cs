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
            WorkingProgramm = new Dictionary<(int Year, int Period), StudentSemester>();
        }

        public IPreferenceStorageService Preference { get; init; }

        public Dictionary<DateOnly, List<StudentTimeTable>> Diary { get; private set; }

        public Dictionary<(int Year, int Period), StudentSemester> WorkingProgramm { get; private set; }

        public void Clear()
        {
            Preference.Clear();
            Barrel.Current.EmptyAll();
            Diary.Clear();
            WorkingProgramm.Clear();
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

        public override async Task<StudentSemester> GetSemester(int year, int period, CancellationToken cancellationToken = default)
        {
            if (WorkingProgramm.ContainsKey((year, period)))
            {
                return WorkingProgramm[(year, period)];
            }

            var selectedSemestr = await base.GetSemester(year, period, cancellationToken);
            WorkingProgramm.Add((year, period), selectedSemestr);

            return selectedSemestr;
        }

        public override async Task<StudentSemester> GetSemester(CancellationToken cancellationToken = default)
        {
            var cacheKey = $"GetCurrentSemester";
            var expireIn = TimeSpan.FromMinutes(10);

            if (Barrel.Current.Exists(cacheKey))
            {
                return Barrel.Current.Get<StudentSemester>(cacheKey);
            }

            try
            {
                var currentSemester = await base.GetSemester(cancellationToken);
                Barrel.Current.Add(cacheKey, currentSemester, expireIn);

                return currentSemester;
            }
            catch (HttpResponseException)
            {
                throw;
            }
        }

        public override async Task<StudentRatingPlan> GetRatingPlan(int disciplineId, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"GetRatingPlan/{disciplineId}";
            var expireIn = TimeSpan.FromMinutes(10);

            if (Barrel.Current.Exists(cacheKey))
            {
                return Barrel.Current.Get<StudentRatingPlan>(cacheKey);
            }

            try
            {
                var myProfile = await base.GetRatingPlan(disciplineId, cancellationToken);
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

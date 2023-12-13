namespace MRSUMobile.MVVM.ViewModel
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.Extensions.Configuration;
    using MRSUMobile.Entities;
    using MRSUMobile.MVVM.Model;
    using MRSUMobile.MVVM.View;
    using MRSUMobile.Services;

    public partial class AppShellViewModel : ObservableObject
    {
        private Preferenses _preferenceConfig;
        private MrsuStorageService _mrsuStorage;

        [ObservableProperty]
        private string _profileShell = "Профиль";

        [ObservableProperty]
        private string _timeTableShell = "Расписание";

        [ObservableProperty]
        private string _academicPerformanceShell = "Успеваемость";

        [ObservableProperty]
        private string _logoutButton = "Выйти";

        [ObservableProperty]
        private User _user;

        public AppShellViewModel(IConfiguration configuration, MrsuApiService mrsuStorageService)
        {
            _preferenceConfig = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
            _mrsuStorage = mrsuStorageService as MrsuStorageService;

            Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                User = await _mrsuStorage.GetMyProfile();
            });
        }

        [RelayCommand]
        private void Logout()
        {
            _mrsuStorage.Clear();
            Application.Current.MainPage = new LoginView();
        }
    }
}
namespace MRSUMobile.MVVM.ViewModel
{
    using CommunityToolkit.Maui.Alerts;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.Extensions.Configuration;
    using MRSUMobile.Entities;
    using MRSUMobile.Exceptions;
    using MRSUMobile.MVVM.Model;
    using MRSUMobile.MVVM.View;
    using MRSUMobile.Services;

    public partial class AppShellViewModel : ObservableObject
    {
        private readonly Preferenses _preferenceConfig;
        private readonly MrsuStorageService _mrsuStorage;

        [ObservableProperty]
        private string _profileShell = "Профиль";

        [ObservableProperty]
        private string _timeTableShell = "Расписание";

        [ObservableProperty]
        private string _academicPerformanceShell = "Успеваемость";

        [ObservableProperty]
        private string _academicCodePlaceholder = "Отметка на занятии";

        [ObservableProperty]
        private string _code;

        [ObservableProperty]
        private string _codePlaceholder = "Код";

        [ObservableProperty]
        private string _sendCodePlaceholder = "✔";

        [ObservableProperty]
        private string _logoutButton = "Выйти";

        [ObservableProperty]
        private User _user;

        public AppShellViewModel(IConfiguration configuration, MrsuApiService mrsuStorageService)
        {
            Routing.RegisterRoute("DisciplinePerfomace", typeof(DisciplinePerfomaceView));

            _preferenceConfig = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
            _mrsuStorage = mrsuStorageService as MrsuStorageService;

            Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                User = await _mrsuStorage.GetMyProfile();
            });
        }

        [RelayCommand]
        private async Task SendCode()
        {
            try
            {
                var result = await _mrsuStorage.SendAttendanceCode(Code);
            }
            catch (HttpResponseException ex)
            {
                await Snackbar.Make(ex.Response.ReasonPhrase.ToString()).Show();
            }
        }

        [RelayCommand]
        private void Logout()
        {
            _mrsuStorage.Clear();
            Application.Current.MainPage = new LoginView();
        }
    }
}
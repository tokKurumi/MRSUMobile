namespace MRSUMobile.MVVM.ViewModel
{
    using CommunityToolkit.Maui.Alerts;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.Extensions.Configuration;
    using MRSUMobile.Entities;
    using MRSUMobile.Exceptions;
    using MRSUMobile.Services;

    public partial class LoginViewModel : ObservableObject
    {
        private Preferenses _preferenceConfig;
        private MrsuStorageService _mrsuStorage;

        [ObservableProperty]
        private string _loginPage = "Вход";

        [ObservableProperty]
        private string _login;

        [ObservableProperty]
        private string _loginPlaceholder = "Логин";

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _passwordPlaceholder = "Пароль";

        [ObservableProperty]
        private string _signInPlaceholder = "Войти";

        [ObservableProperty]
        private string _signInOfflinePlaceholder = "Оффлайн вход";

        [ObservableProperty]
        private string _refreshPasswordPlaceholder = "Забыли пароль?";

        public LoginViewModel(IConfiguration configuration, MrsuApiService mrsuStorageService)
        {
            _preferenceConfig = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
            _mrsuStorage = mrsuStorageService as MrsuStorageService;
        }

        [RelayCommand]
        private async Task Appearing()
        {
            if (_mrsuStorage.Preference.ContainsKey(_preferenceConfig.Token))
            {
                var token = _mrsuStorage.Preference.Get<Token>(_preferenceConfig.Token);
                try
                {
                    _mrsuStorage.SetToken(token);
                    var refreshed = await _mrsuStorage.RefreshSession(token);

                    Application.Current.MainPage = new AppShell();
                }
                catch (HttpResponseException)
                {
                }
            }
        }

        [RelayCommand]
        private async Task SignIn()
        {
            try
            {
                var token = await _mrsuStorage.Autorize(Login, Password);
                _mrsuStorage.SetToken(token);
                Application.Current.MainPage = new AppShell();
            }
            catch (HttpResponseException ex)
            {
                await Snackbar.Make(ex.Response.ReasonPhrase.ToString()).Show();
            }
        }

        [RelayCommand]
        private void RestorePassword()
        {
        }
    }
}
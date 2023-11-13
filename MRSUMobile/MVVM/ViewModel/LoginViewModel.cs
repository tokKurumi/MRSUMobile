using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MRSUMobile.Entities;
using MRSUMobile.Services;
using System.Web.Http;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Maui.Alerts;

namespace MRSUMobile.MVVM.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        Preferenses preferenceConfig;
        MrsuStorageService mrsuStorage;

        public LoginViewModel(IConfiguration configuration, MrsuApiService mrsuStorageService)
        {
            preferenceConfig = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
            mrsuStorage = mrsuStorageService as MrsuStorageService;
        }

        [RelayCommand]
        async Task Appearing()
        {
            if (mrsuStorage.Preference.ContainsKey(preferenceConfig.Token))
            {
                var token = mrsuStorage.Preference.Get<Token>(preferenceConfig.Token);
                try
                {
                    mrsuStorage.SetToken(token);
                    var refreshed = await mrsuStorage.RefreshSession(token);

                    Application.Current.MainPage = new AppShell();
                }
                catch (HttpResponseException)
                {
                }
            }
        }

        [ObservableProperty]
        string loginPage = "Вход";

        [ObservableProperty]
        string login;

        [ObservableProperty]
        string loginPlaceholder = "Логин";

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string passwordPlaceholder = "Пароль";

        [ObservableProperty]
        string signInPlaceholder = "Войти";

        [RelayCommand]
        async Task SignIn()
        {
            try
            {
                var token = await mrsuStorage.Autorize(Login, Password);
                mrsuStorage.SetToken(token);
                Application.Current.MainPage = new AppShell();
            }
            catch (HttpResponseException ex)
            {
                await Snackbar.Make(ex.Response.ReasonPhrase.ToString()).Show();
            }
        }

        [ObservableProperty]
        string signInOfflinePlaceholder = "Оффлайн вход";

        [ObservableProperty]
        string refreshPasswordPlaceholder = "Забыли пароль?";

        [RelayCommand]
        void RestorePassword()
        {
        }
    }
}
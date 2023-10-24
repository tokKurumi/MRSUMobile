using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MRSUMobile.Services;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class LoginViewModel : ObservableObject
	{
		IMrsuApiService mrsuApi;

		public LoginViewModel(IMrsuApiService mrsuApiService)
		{
			mrsuApi = mrsuApiService;
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
			var apiStatus = await mrsuApi.Ping();

			if (apiStatus != System.Net.HttpStatusCode.OK)
			{
				Toast.Make("MRSU is unavailable");
				return;
			}

			var token = await mrsuApi.Autorize(Login, Password);

			if (token is null)
			{
				Toast.Make("Something went wrong while autorizing");
				return;
			}

			mrsuApi.SetToken(token);

			Application.Current.MainPage = new AppShell();
		}

		[ObservableProperty]
		string signInOfflinePlaceholder = "Оффлайн вход";

		[RelayCommand]
		void SignInOffline()
		{
		}

		[ObservableProperty]
		string refreshPasswordPlaceholder = "Забыли пароль?";

		[RelayCommand]
		void RestorePassword()
		{
		}
	}
}
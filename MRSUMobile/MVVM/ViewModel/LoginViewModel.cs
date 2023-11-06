using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MRSUMobile.Helpers;
using MRSUMobile.Entities;
using MRSUMobile.Services;
using System.Web.Http;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Maui.Alerts;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class LoginViewModel : ObservableObject
	{
		Preferenses preferenses;
		IMrsuApiService mrsuApi;

		public LoginViewModel(IConfiguration configuration, IMrsuApiService mrsuApiService)
		{
			preferenses = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
			mrsuApi = mrsuApiService;
		}

		[RelayCommand]
		async Task Appearing()
		{
			if (PreferenceStorageProvider.ContainsKey(preferenses.Token))
			{
				var token = PreferenceStorageProvider.Get<Token>(preferenses.Token);
				try
				{
					var refreshed = await mrsuApi.RefreshSession(token);
					mrsuApi.SetToken(refreshed);
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
				var token = await mrsuApi.Autorize(Login, Password);
				mrsuApi.SetToken(token);
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
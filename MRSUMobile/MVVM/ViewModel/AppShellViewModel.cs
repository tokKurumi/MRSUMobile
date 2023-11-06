using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using MRSUMobile.Entities;
using MRSUMobile.Helpers;
using MRSUMobile.MVVM.Model;
using MRSUMobile.MVVM.View;
using MRSUMobile.Services;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class AppShellViewModel : ObservableObject
	{
		Preferenses preferenses;
		IMrsuApiService mrsuApi;

		public AppShellViewModel(IConfiguration configuration, IMrsuApiService mrsuApiService)
		{
			preferenses = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
			mrsuApi = mrsuApiService;

			Application.Current.Dispatcher.DispatchAsync(async () =>
			{
				User = await mrsuApi.GetMyProfile();
			});
		}

		[ObservableProperty]
		string profileShell = "Профиль";

		[ObservableProperty]
		string timeTableShell = "Расписание";

		[ObservableProperty]
		string logoutButton = "Выйти";

		[ObservableProperty]
		User user;

		[RelayCommand]
		void Logout()
		{
			PreferenceStorageProvider.Clear(preferenses.Token);
			Application.Current.MainPage = new LoginView();
		}
	}
}
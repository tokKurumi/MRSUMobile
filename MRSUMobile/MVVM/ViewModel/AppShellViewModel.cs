using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using MRSUMobile.Entities;
using MRSUMobile.MVVM.Model;
using MRSUMobile.MVVM.View;
using MRSUMobile.Services;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class AppShellViewModel : ObservableObject
	{
		Preferenses preferenceConfig;
		MrsuStorageService mrsuStorage;

		public AppShellViewModel(IConfiguration configuration, MrsuApiService mrsuStorageService)
		{
			preferenceConfig = configuration.GetRequiredSection("Preferenses").Get<Preferenses>();
			mrsuStorage = mrsuStorageService as MrsuStorageService;

			Application.Current.Dispatcher.DispatchAsync(async () =>
			{
				User = await mrsuStorage.GetMyProfile();
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
			mrsuStorage.Preference.Clear(preferenceConfig.Token);
			Application.Current.MainPage = new LoginView();
		}
	}
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MRSUMobile.MVVM.Model;
using MRSUMobile.Services;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class AppShellViewModel : ObservableObject
	{
		IMrsuApiService mrsuApi;

		public AppShellViewModel(IMrsuApiService mrsuApiService)
		{
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
		async Task Logout()
		{
			await Shell.Current.GoToAsync("Logout");
		}
	}
}
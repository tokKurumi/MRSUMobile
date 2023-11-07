using CommunityToolkit.Mvvm.ComponentModel;
using MRSUMobile.MVVM.Model;
using MRSUMobile.Services;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class ProfileViewModel : ObservableObject
	{
		MrsuStorageService mrsuStorage;

		public ProfileViewModel(MrsuApiService mrsuStorageService)
		{
			mrsuStorage = mrsuStorageService as MrsuStorageService;

			Application.Current.Dispatcher.DispatchAsync(async () =>
			{
				User = await mrsuStorage.GetMyProfile();
				ApiStatus = (await mrsuStorage.Ping()).ToString();
			});
		}

		[ObservableProperty]
		User user;

		[ObservableProperty]
		string apiStatus;

		[ObservableProperty]
		string profile = "Профиль";
	}
}
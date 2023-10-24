using CommunityToolkit.Mvvm.ComponentModel;
using MRSUMobile.MVVM.Model;
using MRSUMobile.Services;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class ProfileViewModel : ObservableObject
	{
		IMrsuApiService mrsuApi;

		public ProfileViewModel(IMrsuApiService mrsuApiService)
		{
			mrsuApi = mrsuApiService;

			Application.Current.Dispatcher.DispatchAsync(async () =>
			{
				User = await mrsuApi.GetMyProfile();
				ApiStatus = (await mrsuApi.Ping()).ToString();
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
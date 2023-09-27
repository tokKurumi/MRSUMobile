using MRSUMobile.Services;
using MRSUMobile.Views;
using Xamarin.Forms;

namespace MRSUMobile.ViewModels
{
	public class LoadingViewModel : BindableObject
	{
		public Command PageAppearingCommand { get; }

		public LoadingViewModel()
		{
			PageAppearingCommand = new Command(CheckLoginStatus);
		}

		private async void CheckLoginStatus(object obj)
		{
			if (DependencyService.Get<IMrsuApiService>().IsAutorized())
			{
				await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
			}
			else
			{
				await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
			}
		}
	}
}
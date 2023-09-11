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
			await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
		}
	}
}
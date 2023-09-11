using MRSUMobile.Views;
using Xamarin.Forms;

namespace MRSUMobile.ViewModels
{
	public class LoginViewModel : BindableObject
	{
		public Command LoginCommand { get; }

		public LoginViewModel()
		{
			LoginCommand = new Command(OnLoginClicked);
		}

		private async void OnLoginClicked(object obj)
		{
			await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
		}
	}
}
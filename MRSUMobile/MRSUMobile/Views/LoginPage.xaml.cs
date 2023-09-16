using MRSUMobile.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MRSUMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			this.BindingContext = new LoginViewModel();
		}

		private void Button_Clicked(object sender, System.EventArgs e)
		{
			Navigation.ShowPopupAsync(new ClownPopup());
		}
	}
}
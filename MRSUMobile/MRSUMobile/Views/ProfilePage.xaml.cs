using MRSUMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MRSUMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage()
		{
			InitializeComponent();
			this.BindingContext = new ProfileViewModel();
		}
	}
}
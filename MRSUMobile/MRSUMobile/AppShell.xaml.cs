using Xamarin.Forms;

namespace MRSUMobile
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
		}

		private async void Button_Clicked(object sender, System.EventArgs e)
		{
			await Shell.Current.GoToAsync("//LoginPage");
		}
	}
}
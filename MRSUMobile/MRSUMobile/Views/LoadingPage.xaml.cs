using MRSUMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MRSUMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPage : ContentPage
	{
		public LoadingPage()
		{
			InitializeComponent();
			this.BindingContext = new LoadingViewModel();
		}
	}
}
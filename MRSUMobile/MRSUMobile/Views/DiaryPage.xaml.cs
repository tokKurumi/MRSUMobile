using MRSUMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MRSUMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DiaryPage : ContentPage
	{
		public DiaryPage()
		{
			InitializeComponent();
			this.BindingContext = new DiaryViewModel();
		}
	}
}
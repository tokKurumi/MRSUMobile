using MRSUMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MRSUMobile.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}
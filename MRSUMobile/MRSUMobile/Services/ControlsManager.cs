using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace MRSUMobile.Services
{
	public class ControlsManager : IControlsManager
	{
		private Page currentPage;

		public async Task ShowPopup(Popup popup)
		{
			currentPage = Application.Current?.MainPage ?? throw new NullReferenceException();
			await currentPage.ShowPopupAsync(popup);
		}

		public async Task ShowToast(string message, int duration)
		{
			currentPage = Application.Current?.MainPage ?? throw new NullReferenceException();
			await currentPage.DisplayToastAsync(message, duration);
		}
	}
}
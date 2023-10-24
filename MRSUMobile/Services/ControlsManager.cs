using CommunityToolkit.Maui.Views;

namespace MRSUMobile.Services
{
	public class ControlsManager
	{
		private Page currentPage;

		public async Task ShowPopup(Popup popup)
		{
			currentPage = Application.Current?.MainPage ?? throw new NullReferenceException();
			await currentPage.ShowPopupAsync(popup);
		}

		//public async Task ShowToast(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14)
		//{
		//	Toast.Make(message, duration, textSize);
		//}
	}
}
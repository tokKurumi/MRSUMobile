using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

namespace MRSUMobile.Services
{
	public interface IControlsManager
	{
		Task ShowPopup(Popup popup);
		Task ShowToast(string message, int duration);
	}
}
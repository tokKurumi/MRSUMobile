using MRSUMobile.Helpers;
using MRSUMobile.MVVM.ViewModel;

namespace MRSUMobile.MVVM.View;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();

		BindingContext = ServiceHelper.GetService<LoginViewModel>();
	}
}
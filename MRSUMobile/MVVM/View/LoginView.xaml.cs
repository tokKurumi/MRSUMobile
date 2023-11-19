namespace MRSUMobile.MVVM.View;

using MRSUMobile.Helpers;
using MRSUMobile.MVVM.ViewModel;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<LoginViewModel>();
    }
}
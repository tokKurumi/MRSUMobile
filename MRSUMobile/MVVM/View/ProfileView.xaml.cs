namespace MRSUMobile.MVVM.View;

using MRSUMobile.Helpers;
using MRSUMobile.MVVM.ViewModel;

public partial class ProfileView : ContentPage
{
    public ProfileView()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<ProfileViewModel>();
    }
}
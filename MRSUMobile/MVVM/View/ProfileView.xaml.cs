using MRSUMobile.Helpers;
using MRSUMobile.MVVM.ViewModel;

namespace MRSUMobile.MVVM.View;

public partial class ProfileView : ContentPage
{
    public ProfileView()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<ProfileViewModel>();
    }
}
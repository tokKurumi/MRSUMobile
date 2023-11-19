namespace MRSUMobile
{
    using MRSUMobile.Helpers;
    using MRSUMobile.MVVM.ViewModel;

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = ServiceHelper.GetService<AppShellViewModel>();
        }
    }
}
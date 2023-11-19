namespace MRSUMobile
{
    using MonkeyCache.FileStore;
    using MRSUMobile.MVVM.View;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Barrel.ApplicationId = "MRSUMobile";

            MainPage = new LoginView();
        }
    }
}
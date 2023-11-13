using MonkeyCache.FileStore;
using MRSUMobile.MVVM.View;

namespace MRSUMobile
{
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
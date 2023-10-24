using MRSUMobile.Helpers;
using MRSUMobile.MVVM.View;
using MRSUMobile.MVVM.ViewModel;

namespace MRSUMobile
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			BindingContext = ServiceHelper.GetService<AppShellViewModel>();

			Routing.RegisterRoute("Logout", typeof(LoginView));
		}
	}
}
using Xamarin.Forms;

namespace MRSUMobile.ViewModels
{
	public class ProfileViewModel : BindableObject
	{
		private string _ProfileImagePath = @"https://p.mrsu.ru/Data/imj-content/0/e/0e1b05f9-0023-4998-93a1-950c65f2264f/photo/1-e0b9d2bf-0f5d-4ab8-8376-bbb521ae3327.jpg";
		private string _ProfileUserLogin = "tkurumi901@gmail.com";
		private string _ProfileUserName = "Юдашкин Олег Артёмович";

		public string ProfileImagePath
		{
			get { return _ProfileImagePath; }
			set
			{
				_ProfileImagePath = value;
				OnPropertyChanged("ProfileImagePath");
			}
		}
		public string ProfileUserLogin
		{
			get { return _ProfileUserLogin; }
			set
			{
				_ProfileUserLogin = value;
				OnPropertyChanged(ProfileUserLogin);
			}
		}
		public string ProfileUserName
		{
			get { return _ProfileUserName; }
			set
			{
				_ProfileUserName = value;
				OnPropertyChanged(ProfileUserLogin);
			}
		}

		public ProfileViewModel()
		{
		}
	}
}
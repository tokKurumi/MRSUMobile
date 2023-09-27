using MRSUMobile.Services;
using MRSUMobile.Views;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace MRSUMobile.ViewModels
{
	public class LoginViewModel : BindableObject
	{
		private IMrsuApiService api;
		private IControlsManager controls;

		public static readonly BindableProperty LoginProperty =
			BindableProperty.Create(nameof(Login), typeof(string), typeof(LoginViewModel), default(string));

		public static readonly BindableProperty PasswordProperty =
			BindableProperty.Create(nameof(Password), typeof(string), typeof(LoginViewModel), default(string));

		public Command LoginCommand { get; }

		public Command RestorePasswordCommand { get; }

		public string Login
		{
			get { return (string)GetValue(LoginProperty); }
			set { SetValue(LoginProperty, value); }
		}

		public string Password
		{
			get { return (string)GetValue(PasswordProperty); }
			set { SetValue(PasswordProperty, value); }
		}

		public LoginViewModel()
		{
			api = DependencyService.Get<IMrsuApiService>();
			controls = DependencyService.Get<IControlsManager>();

			LoginCommand = new Command(OnLoginClicked);
			RestorePasswordCommand = new Command(OnRestorePasswordClicked);
		}

		private async void OnLoginClicked(object obj)
		{
			try
			{
				var token = await api.Autorize(Login, Password);
				api.SetToken(token);
				await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
			}
			catch (HttpRequestException ex)
			{
				await controls.ShowToast(ex.Message, 3000);
			}
			catch (Exception ex)
			{
				await controls.ShowToast(ex.Message, 5000);
			}
		}

		private async void OnRestorePasswordClicked(object obj)
		{
			await controls.ShowPopup(new ClownPopup());
		}
	}
}
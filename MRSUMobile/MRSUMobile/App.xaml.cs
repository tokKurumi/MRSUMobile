﻿using MRSUMobile.Services;
using Xamarin.Forms;

namespace MRSUMobile
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.Register<IMrsuApiService, MrsuApiService>();
			DependencyService.Register<IControlsManager, ControlsManager>();

			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}

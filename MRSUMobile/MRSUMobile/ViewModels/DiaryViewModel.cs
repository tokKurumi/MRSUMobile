using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;
using Xamarin.Plugin.Calendar.Enums;
using System.Threading.Tasks;

namespace MRSUMobile.ViewModels
{
	public class DiaryViewModel : BindableObject
	{
		public ICommand DayTappedCommand => new Command<DateTime>(async (date) => await DayTapped(date));

		public CultureInfo Culture => new CultureInfo("ru-RU");

		public DiaryViewModel()
		{
		
		}


		private static async Task DayTapped(DateTime date)
		{
			var message = $"Received tap event from date: {date}";
			await App.Current.MainPage.DisplayAlert("DayTapped", message, "Ok");
		}

	}
}
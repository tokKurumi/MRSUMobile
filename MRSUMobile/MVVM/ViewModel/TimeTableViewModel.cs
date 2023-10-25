using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MRSUMobile.Services;
using System.Globalization;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class TimeTableViewModel : ObservableObject
	{
		IMrsuApiService mrsuApi;

		public TimeTableViewModel(IMrsuApiService mrsuApiService)
		{
			mrsuApi = mrsuApiService;


		}

		[RelayCommand]
		Task DayTapped(DateTime date)
		{
			var message = $"Received tap event from date: {date}";

			return Task.CompletedTask;
		}

		[ObservableProperty]
		CultureInfo culture = new CultureInfo("ru-RU");

		[ObservableProperty]
		string timeTablePage = "Расписание";
	}
}
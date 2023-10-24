using CommunityToolkit.Mvvm.ComponentModel;
using MRSUMobile.Services;
using XCalendar.Core.Models;

namespace MRSUMobile.MVVM.ViewModel
{
	public partial class TimeTableViewModel : ObservableObject
	{
		IMrsuApiService mrsuApi;

		public TimeTableViewModel(IMrsuApiService mrsuApiService)
		{
			mrsuApi = mrsuApiService;

			//Application.Current
		}

		[ObservableProperty]
		Calendar<CalendarDay> timeTable;

		[ObservableProperty]
		string timeTablePage = "Расписание";
	}
}
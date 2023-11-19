namespace MRSUMobile.MVVM.ViewModel
{
    using System.Globalization;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using MRSUMobile.Services;

    public partial class TimeTableViewModel : ObservableObject
    {
        private IMrsuApiService _mrsuApi;

        [ObservableProperty]
        private CultureInfo _culture = new CultureInfo("ru-RU");

        [ObservableProperty]
        private string _timeTablePage = "Расписание";

        public TimeTableViewModel(IMrsuApiService mrsuApiService)
        {
            _mrsuApi = mrsuApiService;
        }

        [RelayCommand]
        private Task DayTapped(DateTime date)
        {
            var message = $"Received tap event from date: {date}";

            return Task.CompletedTask;
        }
    }
}
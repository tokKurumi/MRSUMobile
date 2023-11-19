namespace MRSUMobile.MVVM.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Globalization;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using MRSUMobile.MVVM.Model;
    using MRSUMobile.Services;

    public partial class TimeTableViewModel : ObservableObject
    {
        private MrsuStorageService _mrsuStorage;

        private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);

        [ObservableProperty]
        private CultureInfo _culture = new CultureInfo("ru-RU");

        [ObservableProperty]
        private string _timeTablePage = "Расписание";

        [ObservableProperty]
        private ObservableCollection<StudentTimeTable> _studentTimeTable;

        public TimeTableViewModel(MrsuApiService mrsuStorageService)
        {
            _mrsuStorage = mrsuStorageService as MrsuStorageService;

            Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                StudentTimeTable = new ObservableCollection<StudentTimeTable>(await _mrsuStorage.GetTimeTable(_currentDate));
            });
        }

        [RelayCommand]
        private async Task DayTapped(DateTime date)
        {
            if (DateOnly.FromDateTime(date) == _currentDate)
            {
                return;
            }

            _currentDate = DateOnly.FromDateTime(date);
            StudentTimeTable = new ObservableCollection<StudentTimeTable>(await _mrsuStorage.GetTimeTable(_currentDate));
        }
    }
}
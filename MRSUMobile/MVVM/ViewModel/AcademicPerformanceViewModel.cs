namespace MRSUMobile.MVVM.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MRSUMobile.MVVM.Model;
using MRSUMobile.Services;

public partial class AcademicPerformanceViewModel : ObservableObject
{
    private MrsuStorageService _mrsuStorage;

    private int _semestrsPerYear = 2;

    [ObservableProperty]
    private string _academicPerformancePage = "Успеваемость";

    [ObservableProperty]
    private StudentSemestr _studentSemestr;

    public AcademicPerformanceViewModel(MrsuApiService mrsuStorageService)
    {
        _mrsuStorage = mrsuStorageService as MrsuStorageService;

        Application.Current.Dispatcher.DispatchAsync(async () =>
        {
            StudentSemestr = await _mrsuStorage.GetSemestr(CurrentYear, CurrentPeriod);
        });
    }

    private int CurrentYear { get; set; } = DateTime.Now.Year;

    private int CurrentPeriod { get; set; } = DateTime.Now.Month is >= 3 and <= 9 ? 2 : 1;

    [RelayCommand]
    private async Task NextSemestr()
    {
        if (CurrentPeriod == _semestrsPerYear)
        {
            CurrentPeriod--;
            CurrentYear++;
        }
        else
        {
            CurrentPeriod++;
        }

        StudentSemestr = await _mrsuStorage.GetSemestr(CurrentYear, CurrentPeriod);
    }

    [RelayCommand]
    private async Task PrevSemestr()
    {
        if (CurrentPeriod == 1)
        {
            CurrentPeriod = _semestrsPerYear;
            CurrentYear--;
        }
        else
        {
            CurrentPeriod--;
        }

        StudentSemestr = await _mrsuStorage.GetSemestr(CurrentYear, CurrentPeriod);
    }
}
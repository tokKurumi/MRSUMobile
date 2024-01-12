namespace MRSUMobile.MVVM.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MRSUMobile.MVVM.Model;
using MRSUMobile.Services;
using System.Collections.ObjectModel;

public partial class AcademicPerformanceViewModel : ObservableObject
{
    private readonly MrsuStorageService _mrsuStorage;

    [ObservableProperty]
    private string _academicPerformancePage = "Успеваемость";

    [ObservableProperty]
    private StudentSemester _studentSemestr;

    [ObservableProperty]
    private ObservableCollection<RecordBook> _subjects;

    public AcademicPerformanceViewModel(MrsuApiService mrsuStorageService)
    {
        _mrsuStorage = mrsuStorageService as MrsuStorageService;

        Application.Current.Dispatcher.DispatchAsync(async () =>
        {
            StudentSemestr = await _mrsuStorage.GetSemester();
            Subjects = new ObservableCollection<RecordBook>(StudentSemestr.RecordBooks);
        });
    }

    [RelayCommand]
    private async Task Navigation(object disciplineId)
    {
        await Shell.Current.GoToAsync("DisciplinePerfomace", new Dictionary<string, object>()
        {
            { "DisciplineId", disciplineId },
        });
    }
}
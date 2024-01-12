namespace MRSUMobile.MVVM.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using MRSUMobile.MVVM.Model;
using MRSUMobile.Services;
using System.Collections.Generic;

public partial class DisciplinePerfomaceViewModel : ObservableObject, IQueryAttributable
{
    private readonly MrsuStorageService _mrsuStorage;

    [ObservableProperty]
    private string _disciplinePerfomaceView = "Обзор дисциплины";

    [ObservableProperty]
    private StudentRatingPlan _studentRatingPlan;

    public DisciplinePerfomaceViewModel(MrsuApiService mrsuStorageService)
    {
        _mrsuStorage = mrsuStorageService as MrsuStorageService;
    }

    public int DisciplineId { get; private set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        DisciplineId = query.ContainsKey("DisciplineId") ? Convert.ToInt32(query["DisciplineId"]) : -1;

        Application.Current.Dispatcher.DispatchAsync(async () =>
        {
            StudentRatingPlan = await _mrsuStorage.GetRatingPlan(DisciplineId);
        });
    }
}
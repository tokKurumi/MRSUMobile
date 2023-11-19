namespace MRSUMobile.MVVM.View;

using MRSUMobile.Helpers;
using MRSUMobile.MVVM.ViewModel;

public partial class TimeTableView : ContentPage
{
    public TimeTableView()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<TimeTableViewModel>();
    }
}
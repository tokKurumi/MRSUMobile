using MRSUMobile.Helpers;
using MRSUMobile.MVVM.ViewModel;

namespace MRSUMobile.MVVM.View;

public partial class TimeTableView : ContentPage
{
    public TimeTableView()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<TimeTableViewModel>();
    }
}
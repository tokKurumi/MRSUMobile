namespace MRSUMobile.MVVM.View;

using MRSUMobile.Helpers;
using MRSUMobile.MVVM.ViewModel;

public partial class AcademicPerformanceView : ContentPage
{
    public AcademicPerformanceView()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<AcademicPerformanceViewModel>();
    }
}
namespace MRSUMobile.MVVM.View;

using MRSUMobile.Helpers;
using MRSUMobile.MVVM.ViewModel;

public partial class DisciplinePerfomaceView : ContentPage
{
    public DisciplinePerfomaceView()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.Current.GetService<DisciplinePerfomaceViewModel>();
    }
}
namespace MRSUMobile.MVVM.ViewModel
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using MRSUMobile.MVVM.Model;
    using MRSUMobile.Services;

    public partial class ProfileViewModel : ObservableObject
    {
        private MrsuStorageService _mrsuStorage;

        [ObservableProperty]
        private User _user;

        [ObservableProperty]
        private string _apiStatus;

        [ObservableProperty]
        private string _profile = "Профиль";

        public ProfileViewModel(MrsuApiService mrsuStorageService)
        {
            _mrsuStorage = mrsuStorageService as MrsuStorageService;

            Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                User = await _mrsuStorage.GetMyProfile();
                ApiStatus = (await _mrsuStorage.Ping()).ToString();
            });
        }
    }
}
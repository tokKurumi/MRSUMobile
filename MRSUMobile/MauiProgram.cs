using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MRSUMobile.MVVM.ViewModel;
using MRSUMobile.Services;

namespace MRSUMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Configuration
                .AddJsonStream(FileSystem.OpenAppPackageFileAsync("appsettings.json").Result);

            builder.Services
                .AddSingleton<IMrsuApiService, MrsuApiService>()
                .AddSingleton<IPreferenceStorageService, PreferenceStorageService>()
                .AddSingleton<MrsuApiService, MrsuStorageService>();

            builder.Services
                .AddTransient<LoginViewModel>()
                .AddTransient<AppShellViewModel>()
                .AddTransient<ProfileViewModel>()
                .AddTransient<TimeTableViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
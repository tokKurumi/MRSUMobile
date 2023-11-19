namespace MRSUMobile.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceProvider Current => IPlatformApplication.Current.Services;

        public static TService GetService<TService>()
            => Current.GetService<TService>();
    }
}
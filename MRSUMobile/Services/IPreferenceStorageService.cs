namespace MRSUMobile.Services
{
    public interface IPreferenceStorageService
    {
        void Clear();

        void Clear(string key);

        bool ContainsKey(string key);

        T Get<T>(string key);

        void Set<T>(string key, T data);
    }
}
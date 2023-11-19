namespace MRSUMobile.Services
{
    using Newtonsoft.Json;

    public class PreferenceStorageService : IPreferenceStorageService
    {
        public void Clear()
        {
            Preferences.Default.Clear();
        }

        public void Clear(string key)
        {
            Preferences.Default.Remove(key);
        }

        public bool ContainsKey(string key)
        {
            return Preferences.Default.ContainsKey(key);
        }

        public void Set<T>(string key, T data)
        {
            var serialized = JsonConvert.SerializeObject(data);
            Preferences.Default.Set(key, serialized);
        }

        public T Get<T>(string key)
        {
            var storeage = Preferences.Default.Get(key, string.Empty);
            return JsonConvert.DeserializeObject<T>(storeage);
        }
    }
}
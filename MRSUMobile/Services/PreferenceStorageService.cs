using Newtonsoft.Json;

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

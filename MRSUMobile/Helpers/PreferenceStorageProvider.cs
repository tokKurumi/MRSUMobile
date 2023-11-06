using Newtonsoft.Json;

namespace MRSUMobile.Helpers
{
	public static class PreferenceStorageProvider
	{
		public static void Clear()
		{
			Preferences.Default.Clear();
		}

		public static void Clear(string key)
		{
			Preferences.Default.Remove(key);
		}

		public static bool ContainsKey(string key)
		{
			return Preferences.Default.ContainsKey(key);
		}

		public static void Set<T>(string key, T data)
		{
			var serialized = JsonConvert.SerializeObject(data);
			Preferences.Default.Set(key, serialized);
		}

		public static T Get<T>(string key)
		{
			var storeage = Preferences.Default.Get(key, string.Empty);
			return JsonConvert.DeserializeObject<T>(storeage);
		}
	}
}

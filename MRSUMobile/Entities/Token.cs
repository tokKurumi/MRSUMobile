using System.Text.Json.Serialization;

namespace MRSUMobile.Entities
{
	public class Token
	{
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; }

		[JsonPropertyName("token_type")]
		public string Type { get; set; }

		[JsonPropertyName("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonPropertyName("refresh_token")]
		public string RefreshToken { get; set; }

		private const int auto_expires = 600; // Expire token after 10 mins
		private DateTime create_date = DateTime.Now;
		public bool IsExpired()
		{
			return (DateTime.Now - create_date).Seconds < auto_expires;
		}
	}
}
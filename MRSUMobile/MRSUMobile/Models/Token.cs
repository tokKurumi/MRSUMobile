using System;

namespace MRSUMobile.Models
{
	public class Token
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int expires_in { get; set; }
		public string refresh_token { get; set; }

		private const int auto_expires = 600; // Auto expire token
		private DateTime create_date = DateTime.Now;
		public bool IsExpired()
		{
			return (DateTime.Now - create_date).Seconds > auto_expires;
		}
	}
}
namespace MRSUMobile.Entities
{
    using System.Text.Json.Serialization;

    public class Token
    {
        private const int _autoExpires = 5400; // Expire token after 1.5 hours
        private DateTime _createDate = DateTime.Now;

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string Type { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        public bool IsExpired()
        {
            return (DateTime.Now - _createDate).Seconds < _autoExpires;
        }
    }
}
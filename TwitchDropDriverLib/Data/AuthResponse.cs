using System.Text.Json.Serialization;

namespace TwitchDropDriverLib.Data
{
    public class AuthResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
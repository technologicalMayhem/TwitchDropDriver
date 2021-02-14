using System.Text.Json.Serialization;

namespace TwitchDropDriverLib.Data
{
    public class StreamerData
    {
        [JsonPropertyName("user_login")]
        public string UserLogin { get; set; }

        [JsonPropertyName("game_name")]
        public string GameName { get; set; }

        public override string ToString()
        {
            return UserLogin;
        }
    }
}
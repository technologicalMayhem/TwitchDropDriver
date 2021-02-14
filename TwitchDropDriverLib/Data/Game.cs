using System.Text.Json.Serialization;

namespace TwitchDropDriverLib.Data
{
    public class Game
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("boxArtURL")]
        public string BoxArtUrl { get; set; }
    }
}
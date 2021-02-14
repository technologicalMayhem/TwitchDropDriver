using System.Text.Json.Serialization; 
namespace TwitchDropDriverLib.Data{ 

    public class Channel    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("name")]
        public string Name { get; set; } 

        [JsonPropertyName("url")]
        public string Url { get; set; } 
    }

}
using System;
using System.Text.Json.Serialization; 
namespace TwitchDropDriverLib.Data{ 

    public class UserDropReward    {
        [JsonPropertyName("game")]
        public Game Game { get; set; } 

        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("imageURL")]
        public string ImageURL { get; set; } 

        [JsonPropertyName("isConnected")]
        public bool IsConnected { get; set; } 

        [JsonPropertyName("lastAwardedAt")]
        public DateTime LastAwardedAt { get; set; } 

        [JsonPropertyName("name")]
        public string Name { get; set; } 

        [JsonPropertyName("requiredAccountLink")]
        public string RequiredAccountLink { get; set; } 

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; } 
    }

}
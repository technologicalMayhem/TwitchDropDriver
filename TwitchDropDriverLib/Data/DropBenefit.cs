using System.Text.Json.Serialization; 
namespace TwitchDropDriverLib.Data{ 

    public class DropBenefit    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("imageAssetURL")]
        public string ImageAssetURL { get; set; } 

        [JsonPropertyName("name")]
        public string Name { get; set; } 
    }

}
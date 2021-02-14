using System.Text.Json.Serialization; 
namespace TwitchDropDriverLib.Data{ 

    public class User    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("inventory")]
        public Inventory Inventory { get; set; } 
    }

}
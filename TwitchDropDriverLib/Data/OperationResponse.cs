using System.Text.Json.Serialization; 
namespace TwitchDropDriverLib.Data{ 

    public class OperationResponse    {
        [JsonPropertyName("data")]
        public ResponseData ResponseData { get; set; } 

        [JsonPropertyName("extensions")]
        public ResponseExtensions ResponseExtensions { get; set; } 
    }

}
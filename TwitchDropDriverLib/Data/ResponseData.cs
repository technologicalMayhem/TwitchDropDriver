using System.Text.Json.Serialization; 
namespace TwitchDropDriverLib.Data{ 

    public class ResponseData    {
        [JsonPropertyName("currentUser")]
        public User User { get; set; } 
    }

}
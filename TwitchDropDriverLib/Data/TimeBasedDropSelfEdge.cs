using System.Text.Json.Serialization; 
namespace TwitchDropDriverLib.Data{ 

    public class TimeBasedDropSelfEdge    {
        [JsonPropertyName("isAccountConnected")]
        public bool IsAccountConnected { get; set; } 

        [JsonPropertyName("hasPreconditionsMet")]
        public bool HasPreconditionsMet { get; set; } 

        [JsonPropertyName("currentMinutesWatched")]
        public int CurrentMinutesWatched { get; set; } 

        [JsonPropertyName("isClaimed")]
        public bool IsClaimed { get; set; } 

        [JsonPropertyName("dropInstanceID")]
        public object DropInstanceID { get; set; } 
    }

}
using System.Text.Json.Serialization; 
using System.Collections.Generic; 
namespace TwitchDropDriverLib.Data{ 

    public class Drops    {
        [JsonPropertyName("nodes")]
        public List<object> Nodes { get; set; } 
    }

}
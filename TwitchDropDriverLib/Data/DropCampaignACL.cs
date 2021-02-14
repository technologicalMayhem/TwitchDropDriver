using System.Text.Json.Serialization; 
using System.Collections.Generic; 
namespace TwitchDropDriverLib.Data{ 

    public class DropCampaignACL    {
        [JsonPropertyName("channels")]
        public List<Channel> Channels { get; set; } 
    }

}
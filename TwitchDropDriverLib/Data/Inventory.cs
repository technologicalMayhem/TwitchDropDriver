using System.Text.Json.Serialization; 
using System.Collections.Generic; 
namespace TwitchDropDriverLib.Data{ 

    public class Inventory    {
        [JsonPropertyName("drops")]
        public Drops Drops { get; set; } 

        [JsonPropertyName("dropCampaignsInProgress")]
        public List<DropCampaign> DropCampaignsInProgress { get; set; } 

        [JsonPropertyName("gameEventDrops")]
        public List<UserDropReward> GameEventDrops { get; set; } 
    }

}
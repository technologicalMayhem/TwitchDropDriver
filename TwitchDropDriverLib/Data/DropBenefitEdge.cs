using System.Text.Json.Serialization; 
namespace TwitchDropDriverLib.Data{ 

    public class DropBenefitEdge    {
        [JsonPropertyName("benefit")]
        public DropBenefit DropBenefit { get; set; } 

        [JsonPropertyName("entitlementLimit")]
        public int EntitlementLimit { get; set; } 

        [JsonPropertyName("claimCount")]
        public int ClaimCount { get; set; } 
    }

}
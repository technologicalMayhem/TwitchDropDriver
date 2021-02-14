using System;
using System.Text.Json.Serialization; 
using System.Collections.Generic; 
namespace TwitchDropDriverLib.Data{ 

    public class TimeBasedDrop    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("name")]
        public string Name { get; set; } 

        [JsonPropertyName("startAt")]
        public DateTime StartAt { get; set; } 

        [JsonPropertyName("endAt")]
        public DateTime EndAt { get; set; } 

        [JsonPropertyName("preconditionDrops")]
        public object PreconditionDrops { get; set; } 

        [JsonPropertyName("requiredMinutesWatched")]
        public int RequiredMinutesWatched { get; set; } 

        [JsonPropertyName("benefitEdges")]
        public List<DropBenefitEdge> BenefitEdges { get; set; } 

        [JsonPropertyName("self")]
        public TimeBasedDropSelfEdge TimeBasedDropSelfEdge { get; set; } 

        [JsonPropertyName("campaign")]
        public DropCampaign DropCampaign { get; set; } 
    }

}
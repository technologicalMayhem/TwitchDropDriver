using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace TwitchDropDriverLib.Data
{
    public class DropCampaign
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("accountLinkURL")]
        public string AccountLinkURL { get; set; }

        [JsonPropertyName("startAt")]
        public DateTime StartAt { get; set; }

        [JsonPropertyName("endAt")]
        public DateTime EndAt { get; set; }

        [JsonPropertyName("imageURL")]
        public string ImageURL { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("self")]
        public TimeBasedDropSelfEdge TimeBasedDropSelfEdge { get; set; }

        [JsonPropertyName("game")]
        public Game Game { get; set; }

        [JsonPropertyName("allow")]
        public DropCampaignACL DropCampaignAcl { get; set; }

        [JsonPropertyName("eventBasedDrops")]
        public List<object> EventBasedDrops { get; set; }

        [JsonPropertyName("timeBasedDrops")]
        public List<TimeBasedDrop> TimeBasedDrops { get; set; }
    }
}
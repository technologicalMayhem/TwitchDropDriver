using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwitchDropDriverLib.Data
{
    public class Streams
    {
        [JsonPropertyName("data")]
        public List<StreamerData> Data { get; set; }
    }
}
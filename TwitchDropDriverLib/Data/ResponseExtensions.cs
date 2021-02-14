using System.Text.Json.Serialization;

namespace TwitchDropDriverLib.Data
{
    public class ResponseExtensions
    {
        [JsonPropertyName("durationMilliseconds")]
        public int DurationMilliseconds { get; set; }

        [JsonPropertyName("operationName")]
        public string OperationName { get; set; }

        [JsonPropertyName("requestID")]
        public string RequestID { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace Client.Models.Visits
{
    public class VisitPartDto
    {
        [JsonPropertyName("estimatedTime")]
        public DateTime? EstimatedTime { get; set; }
        [JsonPropertyName("actualTime")]
        public DateTime? ActualTime { get; set; }
        [JsonPropertyName("port")]
        public PortDto Port { get; set; }
    }
}

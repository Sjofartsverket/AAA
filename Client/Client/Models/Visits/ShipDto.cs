using System.Text.Json.Serialization;

namespace Client.Models.Visits
{
    public class ShipDto
    {
        [JsonPropertyName("call_sign")]
        public string CallSign { get; set; }
        [JsonPropertyName("imo")]
        public string Imo { get; set; }
        [JsonPropertyName("mmsi")]
        public string Mmsi { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("flagState")]
        public string FlagState { get; set; }
    }
}

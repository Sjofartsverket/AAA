using System.Text.Json.Serialization;

namespace Client.Models.Visits
{
    public class PortDto
    {
        [JsonPropertyName("unlocode")]
        public string Unlocode { get; set; }
        [JsonPropertyName("port_name")]
        public string PortName { get; set; }
    }
}

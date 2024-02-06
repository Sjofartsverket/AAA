using System.Text.Json.Serialization;

namespace Client.Models.Visits
{
    public class BaseDto
    {
        public List<LinkDto> Links { get; set; }

        public record LinkDto
        {
            [JsonPropertyName("href")]
            public string Href { get; set; }
            [JsonPropertyName("ref")]
            public string Ref { get; set; }
            [JsonPropertyName("method")]
            public string Method { get; set; }
        }
    }
}

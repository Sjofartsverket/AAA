using Client.Models.Pilotages;
using System.Text.Json.Serialization;

namespace Client.Models.Visits
{
    public class VisitDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("ship")]
        public ShipDto Ship { get; set; }
        [JsonPropertyName("lastPortOfCall")]
        public PortDto LastPortOfCall { get; set; }
        [JsonPropertyName("nextPortOfCall")]
        public PortDto NextPortOfCall { get; set; }
        [JsonPropertyName("arrival")]
        public VisitPartDto Arrival { get; set; }
        [JsonPropertyName("departure")]
        public VisitPartDto Departure { get; set; }
        public bool IsExpanded { get; set; }
        public PilotageListDto pilotages { get; set; }

        public void AddLink(string id, Link link)
        {
            Links.Add(id, link);
        }

        public Dictionary<string, Link> Links { get; set; } = new();
    }

    public record VisitListDto
    {
        [JsonPropertyName("visits")]
        public List<VisitDto> Visits { get; set; }
        public void AddLink(string id, Link link)
        {
            Links.Add(id, link);
        }
        [JsonPropertyName("links")]
        public Dictionary<string, Link> Links { get; set; } = new();
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } 
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; } 
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; } 
    }
}

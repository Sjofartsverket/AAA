using Client.Models.Visits;
using System.Text.Json.Serialization;

namespace Client.Models.Pilotages
{
    public record PilotageDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("PlannedStartTime")]
        public DateTime? PlannedStartTime { get; set; }
        [JsonPropertyName("PlannedEndTime")]
        public DateTime? PlannedEndTime { get; set; }
        [JsonPropertyName("ActualStartTime")]
        public DateTime? ActualStartTime { get; set; }
        [JsonPropertyName("ActualEndTime")]
        public DateTime? ActualEndTime { get; set; }
        [JsonPropertyName("FromPointName")]
        public string FromPointName { get; set; }
        [JsonPropertyName("ToPointName")]
        public string ToPointName { get; set; }
        [JsonPropertyName("VisitId")]
        public string VisitId { get; set; } //ItineraryId

        [JsonPropertyName("CurrentShipDraught")]
        public decimal CurrentShipDraught { get; set; } //meter
        [JsonPropertyName("CurrentShipBreadth")]
        public decimal CurrentShipBreadth { get; set; } //meter
        [JsonPropertyName("CurrentShipLoa")]
        public decimal CurrentShipLoa { get; set; } //meter

        [JsonPropertyName("Imo")]
        public string Imo { get; set; }
        [JsonPropertyName("LastUpdated")]
        public DateTime? LastUpdated { get; set; }
        public bool IsExpanded { get; set; }
        public VisitDto visitDto { get; set; }

        public void AddLink(string id, Link link)
        {
            Links.Add(id, link);
        }

        public Dictionary<string, Link> Links { get; set; } = new();
    }

    public record PilotageListDto
    {
        [JsonPropertyName("pilotages")]
        public List<PilotageDto> pilotages { get; set; }
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





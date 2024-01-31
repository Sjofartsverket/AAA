using System.Text.Json.Serialization;

namespace Client.Models.Pilotages
{
    public class MetaDto
    {
        /*
         *  "total_records": 98,
             "page": 99,
             "limit": 20,
             "count": 0
         */
        [JsonPropertyName(name: "total_records")]
        public int TotalRecords { get; set; }
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }

    }
}
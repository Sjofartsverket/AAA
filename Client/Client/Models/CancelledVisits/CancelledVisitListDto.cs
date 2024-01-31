using Client.Models.Pilotages;
using Client.Models.Visits;
using System.Text.Json.Serialization;

namespace Client.Models.CancelledVisits
{
    public class CancelledVisitListDto
    {
        public List<string> CancelledVisits { get; set; }

        public Dictionary<string, Link> Links { get; set; } = new();

    }
}

   

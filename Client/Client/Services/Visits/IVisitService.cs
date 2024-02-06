using Client.Models.Visits;

namespace Client.Services.Visits
{
    public interface IVisitService
    {
        Task<VisitListDto> GetVisits(string href = null); 
        Task<VisitDto> GetPilotageVisit(string visit_id);
    }
}

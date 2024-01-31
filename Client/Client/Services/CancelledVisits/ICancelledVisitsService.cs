using Client.Models.CancelledVisits;
using Client.Models.Visits;

namespace Client.Services.CancelledVisits
{
    public interface ICancelledVisitsService
    {
        Task<CancelledVisitListDto> GetCancelledVisits(string href = null);
    }
}

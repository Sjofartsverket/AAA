using Client.Models;
using Client.Models.Pilotages;
using Client.Models.Visits;
using Client.Pages;

namespace Client.Services.Pilotages
{
    public interface IPilotageService
    {
        Task<PilotageListDto> GetPilotages(string href = null);
        Task<PilotageListDto> GetVisitPilotages(string visit_id);
    }
}

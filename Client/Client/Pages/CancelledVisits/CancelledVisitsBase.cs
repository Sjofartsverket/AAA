using Client.Models.CancelledVisits;
using Client.Models.Pilotages;
using Client.Models.Visits;
using Client.Pages.Visits;
using Client.Services.CancelledVisits;
using Client.Services.Pilotages;
using Client.Services.Visits;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Client.Pages.CancelledVisits
{
    public class CancelledVisitsBase : ComponentBase
    {
        [Inject] public ICancelledVisitsService CancelledVisitsService { get; set; }
        public bool isFetching { get; set; } = true;
        public CancelledVisitListDto cancelledVisits { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isFetching = true;
            errorMessage = "";
            try
            {
                cancelledVisits = await CancelledVisitsService.GetCancelledVisits();
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }
            isFetching = false;
        }
    }
}

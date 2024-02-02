using Microsoft.AspNetCore.Components;
using Client.Models.Pilotages;
using Client.Models.Visits;
using Client.Services.Pilotages;
using Client.Services.Visits;

namespace Client.Pages.Pilotages
{
    public class PilotagesBase : ComponentBase
    {
        [Inject] public IPilotageService PilotageService { get; set; }
        [Inject] public IVisitService VisitService { get; set; }
        public bool isFetching { get; set; } = true;
        public PilotageListDto pilotages { get; set; }
        public VisitListDto visits { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isFetching = true;
            errorMessage = "";
            try
            {
                pilotages = await PilotageService.GetPilotages();
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }
            isFetching = false;
        }

        protected async Task NextPage()
        {
            isFetching = true;
            errorMessage = "";
            try
            {
                if (pilotages.Links.ContainsKey("next"))
                {
                    pilotages = await PilotageService.GetPilotages(pilotages.Links["next"].href);
                }
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }
            isFetching = false;
        }

        protected async Task PreviousPage()
        {
            isFetching = true;
            errorMessage = "";
            try
            {
                if (pilotages.Links.ContainsKey("previous"))
                {
                    pilotages = await PilotageService.GetPilotages(pilotages.Links["previous"].href);
                }
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }
            isFetching = false;
        }

        protected async Task OnExpandedClicked(PilotageDto pilotage)
        {
            errorMessage = "";
            try
            {
                if (pilotage.VisitId == null)
                {
                    errorMessage = "There are no visits associated with this pilotage yet.";
                    return; 
                }

                if (pilotage.visitDto == null)
                {
                    pilotage.visitDto = await VisitService.GetPilotageVisit(pilotage.VisitId);

                    if (pilotage.visitDto == null)
                    {
                        errorMessage = "No visit information found for this pilotage.";
                        return;
                    }
                }

                pilotage.IsExpanded = !pilotage.IsExpanded;
            }
            catch (Exception exc)
            {
                errorMessage = "Visit information could not be fetched: " + exc.Message;
            }
        }
    }
}

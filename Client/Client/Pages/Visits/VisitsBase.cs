using Microsoft.AspNetCore.Components;
using Client.Models.Pilotages;
using Client.Models.Visits;
using Client.Services.Pilotages;
using Client.Services.Visits;

namespace Client.Pages.Visits
{
    public class VisitsBase : ComponentBase
    {
        [Inject] public IVisitService VisitService { get; set; }
        [Inject] public IPilotageService PilotageService { get; set; }
        public bool isFetching { get; set; } = true;
        public VisitListDto visits { get; set; }
        public PilotageListDto pilotages { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isFetching = true;
            errorMessage = "";
            try
            {
                visits = await VisitService.GetVisits();
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
                if (visits.Links.ContainsKey("next"))
                {
                    visits = await VisitService.GetVisits(visits.Links["next"].href);
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
                if (visits.Links.ContainsKey("previous"))
                {
                    visits = await VisitService.GetVisits(visits.Links["previous"].href);
                }
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }
            isFetching = false;
        }

        protected async Task OnExpandedClicked(VisitDto visit)
        {
            errorMessage = "";
            try
            {
                if (visit.pilotages == null)
                {
                    var result = await PilotageService.GetVisitPilotages(visit.Id);

                    if (result != null && result.pilotages != null && result.pilotages.Any())
                    {
                        result.pilotages = result.pilotages.OrderBy(p => p.PlannedStartTime).ToList();
                        visit.pilotages = result;
                    }
                    else
                    {
                        errorMessage = "No pilotages found for this visit.";
                        return;
                    }
                }

                visit.IsExpanded = !visit.IsExpanded;
            }
            catch (Exception exc)
            {
                errorMessage = "An error occurred while expanding the row: " + exc.Message;
            }
        }
    }
}

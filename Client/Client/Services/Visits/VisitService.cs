using Client.Models.Visits;
using Client.Services.Token;
using Client.Utils;
using System.Net.Http.Headers;

namespace Client.Services.Visits
{
    public class VisitService : IVisitService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public VisitService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<VisitListDto> GetVisits(string href = null)
        {
            try
            {
                string sUrl = _httpClient.BaseAddress + $"api/v1/visits?{PaginationHandler.Get(href)}";

                var token = await _tokenService.GetToken();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await _httpClient.GetFromJsonAsync<VisitListDto>(sUrl);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<VisitDto> GetPilotageVisit(string visit_id)
        {
            try
            {
                string sUrl = _httpClient.BaseAddress + $"api/v1/visits/{visit_id}";

                var token = await _tokenService.GetToken();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await _httpClient.GetFromJsonAsync<VisitDto>(sUrl);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
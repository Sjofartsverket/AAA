using Client.Models.CancelledVisits;
using Client.Models.Visits;
using Client.Services.Token;
using Client.Utils;
using System.Net.Http.Headers;

namespace Client.Services.CancelledVisits
{
    public class CancelledVisitsService : ICancelledVisitsService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public CancelledVisitsService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<CancelledVisitListDto> GetCancelledVisits(string href = null)
        {
            try
            {
                string sUrl = _httpClient.BaseAddress + $"api/v1/cancelledvisits?{PaginationHandler.Get(href)}";

                var token = await _tokenService.GetToken();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await _httpClient.GetFromJsonAsync<CancelledVisitListDto>(sUrl);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}

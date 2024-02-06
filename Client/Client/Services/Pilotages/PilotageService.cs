using Client.Models.Pilotages;
using Client.Services.Token;
using Client.Utils;
using System.Net.Http.Headers;

namespace Client.Services.Pilotages
{
    public class PilotageService : IPilotageService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public PilotageService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<PilotageListDto> GetPilotages(string href = null)
        {
            try
            {
                string sUrl = _httpClient.BaseAddress + $"api/v1/pilotages?{PaginationHandler.Get(href)}";

                var token = await _tokenService.GetToken();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await _httpClient.GetFromJsonAsync<PilotageListDto>(sUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PilotageListDto> GetVisitPilotages(string visit_id)
        {
            try
            {
                string sUrl = _httpClient.BaseAddress + $"api/v1/pilotages?{PaginationHandler.Get()}&visit_id={visit_id}";

                var token = await _tokenService.GetToken();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await _httpClient.GetFromJsonAsync<PilotageListDto>(sUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

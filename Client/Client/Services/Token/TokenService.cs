using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Client.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Client.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly ProtectedLocalStorage _storage;
        private readonly NavigationManager _navigationManager;

        public TokenService(HttpClient httpClient, ProtectedLocalStorage storage, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _storage = storage;
            _navigationManager = navigationManager;
        }

        public async Task<string> GetToken()
        {
            return await GetIDPToken();
        }
     

        public async Task<string> GetIDPToken()
        {
            string sUrl = _httpClient.BaseAddress + $"/token";

            string clientId = (await _storage.GetAsync<string>(GlobalVariables.ClientId)).Value ?? "";
            string clientSecret = (await _storage.GetAsync<string>(GlobalVariables.ClientSecret)).Value ?? "";

            var encodedConsumerKey = Uri.EscapeDataString(clientId);
            var encodedConsumerKeySecret = Uri.EscapeDataString(clientSecret);
            var encodedPair = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{encodedConsumerKey}:{encodedConsumerKeySecret}"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(sUrl));
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {encodedPair}");

            request.Content = new StringContent("grant_type=client_credentials&scope=https://snt-public-api.tst.sjofartsverket.se/ https://snt-public-api.tst.sjofartsverket.se/pilotages https://snt-public-api.tst.sjofartsverket.se/visits https://snt-public-api.tst.sjofartsverket.se/cancelledvisits");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var result = await _httpClient.SendAsync(request);
            return GetAccessTokenFromIdpRequest(result.Content.ReadAsStringAsync().Result);
        }

        public async Task<List<Claim>> GetTokenClaims()
        {
            return GetTokenClaims(await GetToken());
        }

        private List<Claim> GetTokenClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return jwtSecurityToken.Claims.ToList();
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private string GetAccessTokenFromIdpRequest(string json) => JsonConvert.DeserializeObject<IdpTokenWrapper>(json).access_token;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        private class IdpTokenWrapper
        {
            public string access_token { get; set; } = "";
        }
    }
}

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

        public TokenService(HttpClient httpClient, ProtectedLocalStorage storage, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _storage = storage;
        }

        // Wrapper for GetIDPToken
        public async Task<string> GetToken()
        {
            return await GetIDPToken();
        }

        // Method to obtain a JWT token from the Identity Provider (IDP)
        public async Task<string> GetIDPToken()
        {
            // Construct the URL for the token endpoint
            string sUrl = _httpClient.BaseAddress + $"/token";

            // Retrieve client credentials (Client ID and Client Secret) from local storage
            string clientId = (await _storage.GetAsync<string>(GlobalVariables.ClientId)).Value ?? "";
            string clientSecret = (await _storage.GetAsync<string>(GlobalVariables.ClientSecret)).Value ?? "";

            // Encode client credentials for Basic Authentication
            var encodedConsumerKey = Uri.EscapeDataString(clientId);
            var encodedConsumerKeySecret = Uri.EscapeDataString(clientSecret);
            var encodedPair = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{encodedConsumerKey}:{encodedConsumerKeySecret}"));

            // Create a POST request to the IDP token endpoint
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(sUrl));

            // Add Basic Authentication header to the request
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {encodedPair}");

            // Set the request content with grant type and scope
            request.Content = new StringContent("grant_type=client_credentials&scope=https://snt-public-api.tst.sjofartsverket.se/ https://snt-public-api.tst.sjofartsverket.se/pilotages https://snt-public-api.tst.sjofartsverket.se/visits https://snt-public-api.tst.sjofartsverket.se/cancelledvisits");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            // Send the request and get the result
            var result = await _httpClient.SendAsync(request);

            // Extract and return the access token from the IDP response
            return GetAccessTokenFromIdpRequest(result.Content.ReadAsStringAsync().Result);
        }

        // Method to get claims from the token
        public async Task<List<Claim>> GetTokenClaims()
        {
            return GetTokenClaims(await GetToken());
        }

        // Method to extract claims from a JWT token
        private List<Claim> GetTokenClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return jwtSecurityToken.Claims.ToList();
        }

        // Method to extract the access token from the IDP response
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private string GetAccessTokenFromIdpRequest(string json) => JsonConvert.DeserializeObject<IdpTokenWrapper>(json).access_token;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        // Class representing the structure of the IDP token response
        private class IdpTokenWrapper
        {
            public string access_token { get; set; } = "";
        }
    }
}

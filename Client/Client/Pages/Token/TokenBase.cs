using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Client.Services.Token;
using Client.Utils;
using System.Security.Claims;

namespace Client.Pages.Token
{
    public class TokenBase : ComponentBase
    {
        [Inject] private ITokenService _tokenService { get; set; }
        [Inject] private ProtectedLocalStorage _storage { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        public string clientId { get; set; }
        public string clientSecret { get; set; }
        
        public bool isFetchingToken { get; set; }

        public string Token { get; set; } = string.Empty;
        public List<Claim> TokenClaims { get; set; } = new List<Claim>();

        public string errorMessage { get; set; } = string.Empty;
        public Dictionary<string, string> validationErrors { get; set; } = new Dictionary<string, string>();

        public async Task HandleSubmit()
        {
            ResetErrorMessage();
            validationErrors.Clear();

            if (string.IsNullOrEmpty(clientId))
            {
                validationErrors.Add("ClientId", "Client ID is required.");
            }

            if (string.IsNullOrEmpty(clientSecret))
            {
                validationErrors.Add("ClientSecret", "Client Secret is required.");
            }

            if (validationErrors.Any() is false)
            {
                isFetchingToken = true;

                try
                {
                    await _storage.SetAsync(@GlobalVariables.ClientId, clientId);
                    await _storage.SetAsync(@GlobalVariables.ClientSecret, clientSecret);

                    Token = await _tokenService.GetToken();
                    TokenClaims = await _tokenService.GetTokenClaims();
                    isFetchingToken = false;
                }
                catch (Exception exc)
                {
                    errorMessage = exc.Message;
                    isFetchingToken = false;
                }
            }
        }

        private void ResetErrorMessage()
        {
            errorMessage = string.Empty;
        }
    }
}

using System.Security.Claims;

namespace Client.Services.Token
{
    public interface ITokenService
    {
        Task<string> GetToken();
        Task<List<Claim>> GetTokenClaims();
    }
}

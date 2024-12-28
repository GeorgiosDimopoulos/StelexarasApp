using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace StelexarasApp.API.Authorization
{
    public interface IAuthTokenProvider
    {
        Task<SecurityToken> GenerateJwtToken();

        Task<string> GetJwtToken(IdentityUser user);
    }
}

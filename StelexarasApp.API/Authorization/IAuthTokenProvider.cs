using Microsoft.IdentityModel.Tokens;

namespace StelexarasApp.API.Authorization
{
    public interface IAuthTokenProvider
    {
        Task<SecurityToken> GetJwtToken();
    }
}

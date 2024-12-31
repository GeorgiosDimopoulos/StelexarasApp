using Microsoft.AspNetCore.Mvc;
using StelexarasApp.API.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace StelexarasApp.API.ApiControllers;

[ApiController]
public class AuthController : ControllerBase
{
    private IAuthTokenProvider _authTokenProvider;

    public AuthController(IAuthTokenProvider authTokenProvider)
    {
        _authTokenProvider = authTokenProvider;
    }

    [HttpGet]
    [Route("api/auth/token")]
    public async Task<string> GetAuthToken()
    {
        var token = await _authTokenProvider.GetJwtToken();
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}

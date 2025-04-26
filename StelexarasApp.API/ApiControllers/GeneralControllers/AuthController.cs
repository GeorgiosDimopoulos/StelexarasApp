using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StelexarasApp.API.Authorization;
using StelexarasApp.Library.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StelexarasApp.API.ApiControllers.GeneralControllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private IAuthTokenProvider _authTokenProvider;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthTokenProvider authTokenProvider, IConfiguration configuration)
    {
        _authTokenProvider = authTokenProvider;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> GetAuthToken([FromQuery] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Password))
            return BadRequest();

        var password = _configuration ["Jwt:Key"];
        if (request.Password.Equals(password))
        {
            var token = await _authTokenProvider.GetJwtToken(request.Password);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Token = tokenString
            });
        }

        return Unauthorized();
    }
}

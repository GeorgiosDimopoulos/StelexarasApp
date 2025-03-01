using Audit.Core;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.API.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace StelexarasApp.API.ApiControllers;

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
    public async Task<IActionResult> GetAuthToken(string input)
    {
        if (string.IsNullOrEmpty(input))
            return BadRequest();

        var password = _configuration ["Jwt:Key"];
        if (input.Equals(password))
        {
            var token = await _authTokenProvider.GetJwtToken(input);
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

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StelexarasApp.API.Authorization;

public class AuthTokenProvider : IAuthTokenProvider
{
    private const int ExpirationMinutes = 180;
    private readonly IConfiguration _configuration;

    public AuthTokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<SecurityToken> GetJwtToken() // IdentityUser user
    {
        var jwtToken = GenerateJwtToken();
        return jwtToken;
    }

    private Task<SecurityToken> GenerateJwtToken()
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

        var jwtSettings = _configuration.GetSection("Jwt") ?? throw new Exception("Jwt section is missing in appsettings.json");
        var userName = jwtSettings ["UserName"]!;
        var userKey = jwtSettings ["Key"] ?? string.Empty;
        var userKeyBytes = Encoding.UTF8.GetBytes(userKey);
        var securityKey = new SymmetricSecurityKey(userKeyBytes);

        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        // var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); 
        var token = new JwtSecurityToken(
            issuer: jwtSettings ["Issuer"],
            audience: jwtSettings ["Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        );

        return Task.FromResult<SecurityToken>(token);
    }
}

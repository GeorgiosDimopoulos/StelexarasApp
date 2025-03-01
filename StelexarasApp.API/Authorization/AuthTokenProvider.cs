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

    public Task<SecurityToken> GetJwtToken(string username)
    {
        var jwtToken = GenerateJwtToken(username);
        return jwtToken;
    }

    private Task<SecurityToken> GenerateJwtToken(string name)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

        var jwtSettings = _configuration.GetSection("Jwt") ?? throw new Exception("Jwt section is missing in appsettings.json");
        var userKey = jwtSettings ["Key"] ?? string.Empty;
        if (userKey.Length < 16)
        {
            userKey = userKey.PadRight(16);
        }

        var userKeyBytes = Encoding.UTF8.GetBytes(userKey);
        var securityKey = new SymmetricSecurityKey(userKeyBytes);

        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, userKey),
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

using Refit;

namespace StelexarasApp.Application.ApiClients;

public interface IAuthClient
{
    [Post("/api/Auth/login")]
    Task<LoginResponse> AuthenticateAsync([Query] LoginRequest request);
}
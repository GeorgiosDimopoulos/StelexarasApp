using Refit;

namespace StelexarasApp.Application.ApiClients;

public interface IAuthClient
{
    [Get("/auth")]
    Task<bool> AuthenticateAsync();
}

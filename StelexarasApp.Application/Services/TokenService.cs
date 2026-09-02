namespace StelexarasApp.Application.Services;

public class TokenService
{
    private string _token;

    public void SaveToken(string token)
    {
        _token = token;
    }

    public string? GetToken()
    {
        return _token;
    }
}

using StelexarasApp.Library.Models.Atoma.Staff;
using System.Net.Http.Json;

namespace StelexarasApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<IStelexos>> GetStelexi()
    {
        var response = await _httpClient.GetAsync("https://localhost:5001/api/stelexi");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<IStelexos>>() ?? [];
    }

    public async Task CreateStelexos(IStelexos stelexos)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/api/stelexi", stelexos);
        response.EnsureSuccessStatusCode();
    }

    // Add more methods for CRUD operations as needed
}
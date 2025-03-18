using StelexarasApp.Library.Models.Atoma.Staff;
using System.Net.Http.Json;

namespace StelexarasApp.Services;

public class ApiService : IApiService
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

    public async Task DeleteStelexos(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:5001/api/stelexi/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateStelexos(IStelexos stelexos)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:5001/api/stelexi/{stelexos.Id}", stelexos);
        response.EnsureSuccessStatusCode();
    }
}
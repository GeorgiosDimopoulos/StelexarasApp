using Refit;
using StelexarasApp.Library.Dtos.People.Staff;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Application.ApiClients;

public interface IStelexiApi
{
    [Get("/Stelexi/Stelexi")]
    Task<List<StelexosResponse>> GetStelexi([Query] StelexosQueryParameters stelexosQueryParameters);

    [Get("/Stelexi/Stelexos/{name}")]
    Task<StelexosResponse> GetStelexos(string name, [Query] StelexosQueryParameters stelexosQueryParameters);

    [Post("/Stelexi/Stelexos")]
    Task<bool> PostStelexos([Body] CreateStelexosRequest stelexosDto);

    [Delete("/Stelexi/Stelexos/{name}")]
    Task<bool> DeleteStelexos(string name);

    [Put("/Stelexi/Stelexos/{id}")]
    Task<bool> UpdateStelexos(int id, [Body] UpdateStelexosRequest stelexosDto);
}

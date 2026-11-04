using Microsoft.AspNetCore.Mvc;
using Refit;
using StelexarasApp.Library.Dtos.People.Staff;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Application.ApiClients;

public interface IStelexiApi
{
    [Get("/Stelexi")]
    Task<IEnumerable<StelexosResponse>> GetStelexi([FromQuery] Thesi? thesi, [Query] StelexosQueryParameters stelexosQueryParameters);

    [Get("/Stelexi/Stelexos/{name}")]
    Task<IEnumerable<StelexosResponse>> GetStelexiByXwro([FromQuery] string name, [Query] StelexosQueryParameters stelexosQueryParameters);

    [Get("/Stelexi/Stelexos/{id}")]
    Task<StelexosResponse> GetStelexosById([Query] Thesi thesi, int id, [Query] StelexosQueryParameters stelexosQueryParameters);

    [Get("/Stelexi/StelexosByName")]
    Task<StelexosResponse> GetStelexosByName([Query] Thesi thesi, [Query] string name, [Query] StelexosQueryParameters stelexosQueryParameters);

    [Post("/Stelexi/Stelexos")]
    Task PostStelexos([Body] CreateStelexosRequest stelexosDto);

    [Delete("/Stelexi/Stelexos/{id}")]
    Task DeleteStelexos(int id);

    [Put("/Stelexi/Stelexos/{id}")]
    Task UpdateStelexos(int id, [Body] UpdateStelexosRequest stelexosDto);
}

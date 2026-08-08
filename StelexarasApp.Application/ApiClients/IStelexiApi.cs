using Refit;
using StelexarasApp.Library.Dtos.People.Staff;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Application.ApiClients;

public interface IStelexiApi
{
    [Get("/Stelexi/Stelexi")]
    Task<List<StelexosResponse>> GetStelexiAsync([Query] StelexosQueryParameters stelexosQueryParameters);

    [Get("/Stelexi/Stelexos/{name}")]
    Task<StelexosResponse> GetStelexos(string name, [Query] StelexosQueryParameters stelexosQueryParameters);

    [Post("/Stelexi/Stelexos")]
    Task<StelexosResponse> PostStelexos([Query] CreateStelexosRequest stelexosDto);

    [Delete("/Stelexi/Stelexos/{name}")]
    Task DeleteStelexos(string name);

    [Put("/Stelexi/Stelexos/{name}")]
    Task PutStelexos(string name, [Body] UpdateStelexosRequest stelexosDto);
}

using Refit;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.Application.ApiClients;

public interface IKoinotitesApi
{
    [Get("/Koinotites/Koinotites")]
    Task<List<KoinotitaResponse>> GetKoinotitesAsync([Query] KoinotitaQueryParameters koinotitaQueryParameters);

    [Get("/Koinotites/Koinotites/{tomeaId}")]
    Task<List<KoinotitaResponse>> GetKoinotitesByTomeaAsync(int tomeaId, [Query] KoinotitaQueryParameters koinotitaQueryParameters);

    [Get("/Koinotites/Koinotita/ByName/{name}")]
    Task<KoinotitaResponse> GetKoinotitaByName(string name, [Query] KoinotitaQueryParameters koinotitaQueryParameters);

    [Get("/Koinotites/Koinotita/ById/{id}")]
    Task<KoinotitaResponse> GetKoinotitaById(int id, [Query] KoinotitaQueryParameters koinotitaQueryParameters);

    [Post("/Koinotites/Koinotita")]
    Task<bool> PostKoinotita([Body] CreateKoinotitaRequest dto);

    [Delete("/Koinotites/Koinotita/{id}")]
    Task<bool> DeleteKoinotita(int id);

    [Put("/Koinotites/Koinotita/{id}")]
    Task<bool> PutKoinotita(int id, [Body] UpdateKoinotitaRequest dto);
}

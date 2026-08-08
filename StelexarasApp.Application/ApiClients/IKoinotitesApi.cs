using Refit;
using StelexarasApp.Library.Dtos.Domi;

namespace StelexarasApp.Application.ApiClients;

public interface IKoinotitesApi
{
    [Get("/Koinotites/Koinotites")]
    Task<List<KoinotitaResponse>> GetKoinotitesAsync();

    [Get("/Koinotites/Koinotites/{tomeaId}")]
    Task<List<KoinotitaResponse>> GetKoinotitesByTomeaAsync(int tomeaId);

    [Get("/Koinotites/Koinotita/ByName/{name}")]
    Task<KoinotitaResponse> GetKoinotitaByName(string name);

    [Get("/Koinotites/Koinotita/ById/{id}")]
    Task<KoinotitaResponse> GetKoinotitaById(int id);

    [Post("/Koinotites/Koinotita")]
    Task<bool> PostKoinotita([Body] CreateKoinotitaRequest dto);

    [Delete("/Koinotites/Koinotita/{id}")]
    Task<bool> DeleteKoinotita(int id);

    [Put("/Koinotites/Koinotita/{id}")]
    Task<bool> PutKoinotita(int id, [Body] UpdateKoinotitaRequest dto);
}

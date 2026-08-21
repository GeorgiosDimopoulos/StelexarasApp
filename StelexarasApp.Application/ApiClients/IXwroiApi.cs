using Refit;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.Application.ApiClients;

public interface IXwroiApi
{
    [Get("/Skines/Skines")]
    Task<List<SkiniResponse>> GetSkinesAsync([Query] SkiniQueryParameters skinesQueryParameters);

    [Get("/Skines/Skini/{id}")]
    Task<SkiniResponse> GetSkiniById(int id, [Query] SkiniQueryParameters skiniQueryParameters);

    [Get("/Skines/Skini/{name}")]
    Task<SkiniResponse> GetSkiniByName(string name, [Query] SkiniQueryParameters skiniQueryParameters);

    [Get("/Skines/Skines/ByKoinotitaName/{koinotitaName}")]
    Task<List<SkiniResponse>> GetSkinesByKoinotitaName(string koinotitaName, [Query] SkiniQueryParameters skiniQueryParameters);

    [Get("/Skines/Skines/ByKoinotitaId/{koinotitaId}")]
    Task<List<SkiniResponse>> GetSkinesByKoinotitaId(int koinotitaId, [Query] SkiniQueryParameters skiniQueryParameters);

    [Get("/Skines/SkinesEkpaideuomenon")]
    Task<List<SkiniResponse>> GetSkinesEkpaideuomenon();

    [Post("/Skines/Skini")]
    Task<bool> PostSkini([Body] CreateSkiniRequest skiniDto);

    [Delete("/Skines/Skini/{id}")]
    Task<bool> DeleteSkini(int id);

    [Put("/Skines/Skini/{id}")]
    Task<bool> PutSkini(int id, [Body] UpdateSkiniRequest skiniDto);


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


    [Get("/Tomeis/Tomeis")]
    Task<List<TomeasResponse>> GetTomeisAsync([Query] TomeasQueryParameters tomeasQueryParameters);

    [Get("/Tomeis/Tomea/{name}")]
    Task<TomeasResponse> GetTomea(string name, [Query] TomeasQueryParameters tomeasQueryParameters);

    [Get("/AnwtatoiXwroi")]
    Task<List<string>> GetAnwtatoiXwroiAsync();


    [Post("/Tomeis/Tomea")]
    Task<TomeasResponse> PostTomea([Query] CreateTomeasRequest tomeasDto);

    [Delete("/Tomeis/Tomea/{name}")]
    Task DeleteTomea(string name);

    [Put("/Tomeis/Tomea/{name}")]
    Task PutTomea(string name, [Body] UpdateTomeasRequest tomeasDto);
}

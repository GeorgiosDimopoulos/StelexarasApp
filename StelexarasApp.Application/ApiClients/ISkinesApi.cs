using Refit;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.Application.ApiClients;

public interface ISkinesApi
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
}

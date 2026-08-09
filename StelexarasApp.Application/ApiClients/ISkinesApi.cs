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
    Task<List<SkiniResponse>> GetSkinesByKoinotitaNameAsync(string koinotitaName);

    [Get("/Skines/Skines/ByKoinotitaId/{koinotitaId}")]
    Task<List<SkiniResponse>> GetSkinesByKoinotitaIdAsync(int koinotitaId);

    [Get("/Skines/SkinesEkpaideuomenon")]
    Task<List<SkiniResponse>> GetSkinesEkpaideuomenonAsync();

    [Post("/Skines/Skini")]
    Task<SkiniResponse> PostSkini([Body] CreateSkiniRequest skiniDto);

    [Delete("/Skines/Skini/{id}")]
    Task DeleteSkini(int id);

    [Put("/Skines/Skini/{id}")]
    Task PutSkini(int id, [Body] UpdateSkiniRequest skiniDto);
}

using Refit;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.Application.ApiClients;

public interface ISkinesApi
{
    [Get("/Skines/Skines")]
    Task<List<SkiniResponse>> GetSkinesAsync([Query] SkiniQueryParameters skinesQueryParameters);

    [Get("Skines//Skini/{name}")]
    Task<SkiniResponse> GetSkini(string name, [Query] SkiniQueryParameters skiniQueryParameters);

    [Post("Skines//Skini")]
    Task<SkiniResponse> PostSkini([Query] CreateSkiniRequest skiniDto);

    [Delete("Skines//Skini/{id}")]
    Task DeleteSkini(int id);

    [Put("Skines//Skini/{id}")]
    Task PutSkini(int id, [Body] UpdateSkiniRequest skiniDto);

    [Get("/Skines/Skines/{koinotitaName}")]
    Task<List<SkiniResponse>> GetSkinesByKoinotitaAsync(string koinotitaName);

    [Get("/Skines/SkinesEkpaideuomenon")]
    Task<List<SkiniResponse>> GetSkinesEkpaideuomenonAsync();
}

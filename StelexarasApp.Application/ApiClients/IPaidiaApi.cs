using Refit;
using StelexarasApp.Library.Dtos.People.Children;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Application.ApiClients;

public interface IPaidiaApi
{
    [Get("/Paidia")]
    Task<List<PaidiResponse>> GetPaidiaAsync([Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/SearchByName/{name}")]
    Task<List<PaidiResponse>> SearchPaidiaByName(string name, [Query] PaidiQueryParameters paidiQueryParameters); 

    [Get("/Paidia/{id}")]
    Task<PaidiResponse> GetPaidi(int id, [Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/{name}")]
    Task<PaidiResponse> GetPaidiByName(string name, [Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/Koinotita/ById/{id}")]
    Task<List<PaidiResponse>> GetPaidiaByKoinotitaId(int id, [Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/Koinotita/ByName/{name}")]
    Task<List<PaidiResponse>> GetPaidiaByKoinotitaName(string name, [Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/BySkiniId/{id}")]
    Task<List<PaidiResponse>> GetPaidiaBySkiniId(int id, [Query] PaidiQueryParameters paidiQueryParameters);

    [Post("/Paidia")]
    Task<bool> PostPaidi([Body] CreatePaidiRequest paidiDto);

    [Delete("/Paidia/{id}")]
    Task<bool> DeletePaidi(int id);

    [Put("/Paidia/Paidi/{id}")]
    Task<bool> UpdatePaidi(int id, [Body] UpdatePaidiRequest paidiDto);
}

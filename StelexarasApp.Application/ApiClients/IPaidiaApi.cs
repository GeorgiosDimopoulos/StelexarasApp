using Refit;
using StelexarasApp.Library.Dtos.People.Children;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Application.ApiClients;

public interface IPaidiaApi
{
    [Get("/Paidia")]
    Task<List<PaidiResponse>> GetPaidiaAsync([Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/{id}")]
    Task<PaidiResponse> GetPaidi(int id, [Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/Koinotita/{name}")]
    Task<List<PaidiResponse>> GetPaidiaByKoinotitaName(string name, [Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/BySkiniId/{id}")]
    Task<List<PaidiResponse>> GetPaidiaBySkiniId(int id, [Query] PaidiQueryParameters paidiQueryParameters);

    [Post("/Paidia")]
    Task<PaidiResponse> PostPaidi([Body] CreatePaidiRequest paidiDto);

    [Delete("/Paidia/{id}")]
    Task DeletePaidi(int id);

    [Put("/Paidia/{id}")]
    Task PutPaidi(int id, [Body] UpdatePaidiRequest paidiDto);
}

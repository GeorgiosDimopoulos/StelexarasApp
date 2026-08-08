using Refit;
using StelexarasApp.Library.Dtos.People.Children;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Application.ApiClients;

public interface IPaidiaApi
{
    [Get("/Paidia/Paidia")]
    Task<List<PaidiResponse>> GetPaidiaAsync([Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/Paidi/{name}")]
    Task<PaidiResponse> GetPaidi(string name, [Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/Koinotita/{name}")]
    Task<List<PaidiResponse>> GetPaidiaByKoinotitaName(string name, [Query] PaidiQueryParameters paidiQueryParameters);

    [Get("/Paidia/Skini/{id}")]
    Task<List<PaidiResponse>> GetPaidiaBySkiniId(int id, [Query] PaidiQueryParameters paidiQueryParameters);

    [Post("/Paidia/Paidi")]
    Task<PaidiResponse> PostPaidi([Query] CreatePaidiRequest paidiDto);

    [Delete("/Paidia/Paidi/{name}")]
    Task DeletePaidi(string name);

    [Put("/Paidia/Paidi/{name}")]
    Task PutPaidi(string name, [Body] UpdatePaidiRequest paidiDto);
}

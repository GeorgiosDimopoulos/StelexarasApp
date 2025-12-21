using Refit;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.Application.ApiClients;

public interface ITomeisApi
{
    [Get("/Tomeis/Tomeis")]
    Task<List<TomeasResponse>> GetTomeisAsync([Query] TomeasQueryParameters tomeasQueryParameters);

    [Get("/Tomeis/Tomea/{name}")]
    Task<TomeasResponse> GetTomea(string name, [Query] TomeasQueryParameters tomeasQueryParameters);

    [Post("/Tomeis/Tomea")]
    Task<TomeasResponse> PostTomea([Query] CreateTomeasRequest tomeasDto);

    [Delete("/Tomeis/Tomea/{name}")]
    Task DeleteTomea(string name);

    [Put("/Tomeis/Tomea/{name}")]
    Task PutTomea(string name, [Body] UpdateTomeasRequest tomeasDto);
}

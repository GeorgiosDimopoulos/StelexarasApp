using Refit;
using StelexarasApp.Library.Dtos.People;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Application.ApiClients;

public interface IParentsApi
{
    [Get("/Parents")]
    Task<List<ParentResponse>> GetParentsAsync([Query] ParentQueryParameters parentQueryParameters);

    [Get("/Parents/{id}")]
    Task<ParentResponse> GetParent(int id, [Query] ParentQueryParameters parentQueryParameters);

    [Post("/Parents")]
    Task<bool> PostParent([Body] CreateParentRequest parentDto);

    [Delete("/Parents/{id}")]
    Task<bool> DeleteParent(int id);

    [Put("/Parents/{id}")]
    Task<bool> UpdateParent(int id, [Body] UpdateParentRequest parentDto);
}

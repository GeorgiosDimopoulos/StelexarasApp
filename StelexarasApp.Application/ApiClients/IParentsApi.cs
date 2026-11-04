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
    
    [Get("/Parents/ByName/{name}")]
    Task<ParentResponse> GetParentByName(string name, [Query] ParentQueryParameters parentQueryParameters);

    [Get("/Parents/ByChild/{childId}")]
    Task<ParentResponse> GetParentByChildId(int childId, [Query] ParentQueryParameters parentQueryParameters);

    [Post("/Parents")]
    Task PostParent([Body] CreateParentRequest parentDto);

    [Delete("/Parents/{id}")]
    Task DeleteParent(int id);

    [Put("/Parents/{id}")]
    Task UpdateParent(int id, [Body] UpdateParentRequest parentDto);
}

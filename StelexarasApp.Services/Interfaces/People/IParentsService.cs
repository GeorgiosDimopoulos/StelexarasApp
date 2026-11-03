using FluentResults;
using StelexarasApp.Library.Dtos.People;

namespace StelexarasApp.Services.Interfaces.People;

public interface IParentsService
{
    Task<Result<IEnumerable<ParentResponse>>> GetParentsInService(ParentQueryParameters parentQueryParameters);
    Task<Result<ParentResponse>> GetParentByChildIdInService(int childId, ParentQueryParameters parentQueryParameters);
    Task<Result<ParentResponse>> GetParentByIdInService(int id, ParentQueryParameters parentQueryParameters);
    Task<Result<ParentResponse>> GetParentByNameInService(string name, ParentQueryParameters parentQueryParameters);

    Task<Result> CreateParentInService(CreateParentRequest request);
    Task<Result> UpdateParentInService(int id, UpdateParentRequest request);
    Task<Result> DeleteParentInService(int id);
}
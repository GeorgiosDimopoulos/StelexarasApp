using StelexarasApp.Library.Models.Atoma;

namespace StelexarasApp.DataAccess.Repositories.IRepositories;

public interface IParentsRepository
{
    Task<IEnumerable<Parent>> GetParentsInDb(ParentQueryParameters? queryParameters);
    Task<Parent> GetParentByIdInDb(int id, ParentQueryParameters? parentQueryParameters);
    Task<Parent> GetParentByNameInDb(string name, ParentQueryParameters? parentQueryParameters);
    Task<Parent> GetParentByChildIdInDb(int childId, ParentQueryParameters? parentQueryParameters);

    Task<bool> AddParentInDb(Parent p);
    Task<bool> UpdateParentInDb(int id, Parent p);
    Task<bool> DeleteParentInDb(int id);
}
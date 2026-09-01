namespace StelexarasApp.DataAccess.Repositories.IRepositories;

public interface IStaffRepository
{
    Task<IEnumerable<IStelexos>> GetStelexiInDb(Thesi? thesi, StelexosQueryParameters? queryParameters);
    Task<IEnumerable<IStelexos>> GetStelexoiAnaXwroInDb(string? xwrosName, StelexosQueryParameters? queryParameters);    
    Task<IStelexos> GetStelexosByIdInDb(int id);
    Task<IStelexos> GetStelexosByNameInDb(string name, StelexosQueryParameters? stelexosQueryParameters);
    
    Task<bool> AddStelexosInDb(IStelexos stelexos);
    Task<bool> UpdateStelexosInDb(int id, IStelexos stelexos);
    Task<bool> DeleteStelexosInDb(int id);

    Task<bool> HasPlaceAnotherStelexosInDb(Thesi thesi, int id, string newSkiniName);
}
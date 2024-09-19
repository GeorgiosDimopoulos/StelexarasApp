using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Stelexos>> GetStelexoiAnaXwroInDb(Thesi thesi, string? xwrosName);

        Task<Stelexos> GetStelexosByIdInDb(int id, Thesi? thesi);
        Task<Stelexos> GetStelexosByNameInDb(string name, Thesi? thesi); 

        Task<bool> AddStelexosInDb(Stelexos stelexos);

        Task<bool> UpdateStelexosInDb(Stelexos stelexos);

        Task<bool> DeleteStelexosInDb(int id, Thesi thesi);

        Task<bool> MoveOmadarxisToAnotherSkiniInDb(int id, string newSkiniName);
    }
}

using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Stelexos>> GetStelexoiAnaXwroInDb(Thesi thesi, string? xwrosName);

        Task<Stelexos> FindStelexosByIdInDb(int id, Thesi thesi);

        Task<bool> AddStelexosInDb(Stelexos stelexos);

        Task<bool> UpdateStelexosInDb(Stelexos stelexos);

        Task<bool> DeleteStelexosInDb(int id, Thesi thesi);

        Task<bool> MoveOmadarxisToAnotherSkiniInDb(int id, int newSkiniId);
    }
}

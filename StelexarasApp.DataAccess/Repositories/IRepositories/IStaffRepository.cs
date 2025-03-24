using StelexarasApp.Library.Models.Atoma.Staff;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IStaffRepository
    {
        Task<IEnumerable<IStelexos>> GetStelexoiAnaXwroInDb(Thesi thesi, string? xwrosName);
        Task<IStelexos> GetStelexosByIdInDb(int id);
        Task<IStelexos> GetStelexosByNameInDb(string name, Thesi? thesi);

        //Task<bool> AddOmadarxiInDb(Omadarxis omadarxis);
        //Task<bool> AddKoinotarxiInDb(Koinotarxis koinotarxis);
        //Task<bool> AddTomearxiInDb(Tomearxis tomearxis);
        Task<bool> AddStelexosInDb(IStelexos stelexos);

        Task<bool> UpdateStelexosInDb(int id, IStelexos stelexos);
        Task<bool> DeleteStelexosInDb(int id);

        Task<bool> MoveOmadarxisToAnotherSkiniInDb(int id, string newSkiniName);
    }
}
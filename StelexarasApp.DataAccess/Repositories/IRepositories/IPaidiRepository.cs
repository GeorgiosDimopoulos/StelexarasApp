using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IPaidiRepository
    {
        Task<Paidi> FindPaidiAsync(int id);
        Task<bool> RemovePaidiAsync(Paidi paidi);
        Task<bool> SaveChangesAsync();

        Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId);

        Task<bool> AddPaidiInDbAsync(Paidi paidi);

        Task<bool> AddSkinesInDb(Skini skini);

        Task<bool> DeletePaidiInDb(Paidi paidi);

        Task<bool> UpdatePaidiInDb(Paidi paidi);

        Task<Paidi> GetPaidiById(int id);

        Task<Skini> GetSkiniByName(string fullName);

        Task<IEnumerable<Skini>> GetSkines();
        Task<IEnumerable<Paidi>> GetPaidia(PaidiType type);
    }
}

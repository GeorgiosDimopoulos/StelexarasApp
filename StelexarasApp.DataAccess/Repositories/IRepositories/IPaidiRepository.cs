using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IPaidiRepository
    {
        Task<bool> SaveChangesInDb();

        Task<bool> MovePaidiToNewSkiniInDb(int paidiId, int newSkiniId);

        Task<bool> AddPaidiInDb(Paidi paidi);

        Task<bool> AddSkinesInDb(Skini skini);

        Task<bool> DeletePaidiInDb(Paidi paidi);

        Task<bool> UpdatePaidiInDb(Paidi paidi);

        Task<Paidi> GetPaidiByIdFromDb(int id);

        Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType type);
    }
}

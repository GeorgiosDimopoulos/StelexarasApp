using StelexarasApp.Library.Models.Atoma.Children;

namespace StelexarasApp.DataAccess.Repositories.IRepositories;

public interface IPaidiRepository
{
    Task<bool> MovePaidiToNewSkiniInDb(int paidiId, int newSkiniId);

    Task<bool> AddPaidiInDb(Paidi paidi);

    Task<bool> AddSkinesInDb(Skini skini);

    Task<bool> DeletePaidiInDb(Paidi paidi);

    Task<bool> UpdatePaidiInDb(Paidi paidi);

    Task<Paidi> GetPaidiByIdFromDb(int id);
    Task<IEnumerable<Paidi>> GetPaidiaInSkiniFromDb(string n);
    Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaFromDb(string n);
    Task<IEnumerable<Paidi>> GetPaidiaInSxoliFromDb();
    Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType? type);
}

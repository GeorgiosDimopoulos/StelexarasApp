namespace StelexarasApp.DataAccess.Repositories.IRepositories;

public interface IPaidiaRepository
{
    Task<bool> MovePaidiToNewSkiniInDb(int paidiId, int newSkiniId);

    Task<bool> AddPaidiInDb(Paidi paidi, string skini);

    Task<bool> AddSkinesInDb(Skini skini);

    Task<bool> DeletePaidiInDb(int id);

    Task<bool> UpdatePaidiInDb(Paidi paidi);

    Task<Paidi> GetPaidiByIdFromDb(int id);
    Task<IEnumerable<Paidi>> GetPaidiaInSkiniFromDb(string n);
    Task<IEnumerable<Paidi>> GetPaidiaInSkiniIdFromDb(int id); 
    Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaFromDb(string n);
    Task<IEnumerable<Paidi>> GetPaidiaInSxoliFromDb();
    Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType? type);
}

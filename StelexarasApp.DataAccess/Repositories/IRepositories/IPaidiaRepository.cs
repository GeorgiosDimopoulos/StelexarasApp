namespace StelexarasApp.DataAccess.Repositories.IRepositories;

public interface IPaidiaRepository
{
    Task<bool> MovePaidiToNewSkiniInDb(int paidiId, int newSkiniId);

    Task<bool> AddPaidiInDb(Paidi paidi, string skini);

    Task<bool> AddSkinesInDb(Skini skini);

    Task<bool> DeletePaidiInDb(int id);

    Task<bool> UpdatePaidiInDb(Paidi paidi);

    Task<Paidi> GetPaidiByIdFromDb(int id, PaidiQueryParameters queryParameters);
    Task<IEnumerable<Paidi>> GetPaidiaInSkiniFromDb(string n, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<Paidi>> GetPaidiaInSkiniIdFromDb(int id, PaidiQueryParameters paidiQueryParameters); 
    Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaFromDb(string n, PaidiQueryParameters queryParameters);
    Task<IEnumerable<Paidi>> GetPaidiaInSxoliFromDb(PaidiQueryParameters queryParameters);
    Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType? type, PaidiQueryParameters queryParameters);
}

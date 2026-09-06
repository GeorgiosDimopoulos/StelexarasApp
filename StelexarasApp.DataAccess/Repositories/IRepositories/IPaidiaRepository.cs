namespace StelexarasApp.DataAccess.Repositories.IRepositories;

public interface IPaidiaRepository
{
    Task<Paidi> GetPaidiByIdFromDb(int id, PaidiQueryParameters queryParameters);
    Task<Paidi> GetPaidiByNameFromDb(string name, PaidiQueryParameters queryParameters);
    Task<Skini> GetPaidiSkiniByNameIdFromDb(string skiniName);

    Task<IEnumerable<Paidi>> GetPaidiaInSkiniFromDb(string n, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<Paidi>> GetPaidiaInSkiniIdFromDb(int id, PaidiQueryParameters paidiQueryParameters); 
    Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaIdFromDb(int id, PaidiQueryParameters queryParameters);
    Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaNameFromDb(string n, PaidiQueryParameters queryParameters);
    Task<IEnumerable<Paidi>> GetPaidiaInSxoliFromDb(PaidiQueryParameters queryParameters);
    Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType? type, PaidiQueryParameters queryParameters);        
    Task<IEnumerable<Paidi>> GetPaidiaByNameFromDb(string name, PaidiQueryParameters queryParameters);

    Task<bool> AddPaidiInSkini(Paidi paidi, string skini);

    Task<bool> DeletePaidiInDb(int id);

    Task<bool> UpdatePaidiInDb(int id, Paidi paidi);
}

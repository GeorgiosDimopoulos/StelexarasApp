namespace StelexarasApp.Services.Interfaces.People;

public interface IPaidiService<TCreate, TUpdate, TDelete, TResponse>
{
    Task<bool> CreatePaidiInService(TCreate request);
    Task<bool> UpdatePaidiInService(TUpdate request);
    Task<bool> DeletePaidiInService(TDelete request);
    Task<TResponse> GetPaidiByIdInService(int id);
    Task<IEnumerable<TResponse>> GetPaidiaInService(PaidiType? paidiType);
    Task<IEnumerable<TResponse>> GetPaidiaBySkiniInService(string skini);
    Task<IEnumerable<TResponse>> GetPaidiaBySkiniIdInService(int skiniId);
    Task<IEnumerable<TResponse>> GetPaidiaByKoinotitaInService(string koinotita);
    Task<IEnumerable<TResponse>> GetPaidiaBySxoliInService();
}

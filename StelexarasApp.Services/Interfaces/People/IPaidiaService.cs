namespace StelexarasApp.Services.Interfaces.People;

public interface IPaidiaService<TCreate, TUpdate, TResponse>
{
    Task<bool> CreatePaidiInService(TCreate request);
    Task<bool> UpdatePaidiInService(TUpdate request);
    Task<bool> DeletePaidiInService(int id);
    Task<TResponse> GetPaidiByIdInService(int id, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<TResponse>> GetPaidiaInService(PaidiType? paidiType, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<TResponse>> GetPaidiaBySkiniInService(string skini, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<TResponse>> GetPaidiaBySkiniIdInService(int skiniId, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<TResponse>> GetPaidiaByKoinotitaInService(string koinotita, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<TResponse>> GetPaidiaBySxoliInService(PaidiQueryParameters paidiQueryParameters);
}

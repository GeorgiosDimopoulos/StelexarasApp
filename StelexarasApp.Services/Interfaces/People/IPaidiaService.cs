using FluentResults;

namespace StelexarasApp.Services.Interfaces.People;

public interface IPaidiaService
{
    Task<Result<PaidiResponse>> GetPaidiByIdInService(int id, PaidiQueryParameters paidiQueryParameters);
    Task<Result<PaidiResponse>> GetPaidiByNameInService(string name, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<PaidiResponse>> GetPaidiaInService(PaidiType? paidiType, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<PaidiResponse>> GetPaidiaBySkiniInService(string skini, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<PaidiResponse>> GetPaidiaBySkiniIdInService(int skiniId, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<PaidiResponse>> GetPaidiaByKoinotitaNameInService(string koinotita, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<PaidiResponse>> GetPaidiaByKoinotitaIdInService(int id, PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<PaidiResponse>> GetPaidiaBySxoliInService(PaidiQueryParameters paidiQueryParameters);
    Task<IEnumerable<PaidiResponse>> GetPaidiaByNameInService(string name, PaidiQueryParameters paidiQueryParameters);

    Task<Result> CreatePaidiInService(CreatePaidiRequest request);
    Task<Result> UpdatePaidiInService(int id, UpdatePaidiRequest request);
    Task<Result> DeletePaidiInService(int id);
}

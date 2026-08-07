using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.Services.IServices;

public interface ITeamsService
{
    Task<bool> HasData();
    Task<bool> AddSkiniInService(CreateSkiniRequest skini);
    Task<bool> AddKoinotitaInService(CreateKoinotitaRequest koinotita);
    Task<bool> AddTomeasInService(CreateTomeasRequest tomeas);

    Task<bool> DeleteSkiniInService(int skiniId);
    Task<bool> DeleteKoinotitaInService(int koinotitaId);
    Task<bool> DeleteTomeasInService(string n);

    Task<bool> UpdateSkiniInService(int id, UpdateSkiniRequest skini);
    Task<bool> UpdateKoinotitaInService(int id, UpdateKoinotitaRequest koinotita);
    Task<bool> UpdateTomeaInService(string id, UpdateTomeasRequest tomeas);

    Task<IEnumerable<SkiniResponse>> GetAllSkinesInService(SkiniQueryParameters? skiniQueryParameters);
    Task<IEnumerable<SkiniResponse>> GetSkinesAnaKoinotitaNameInService(SkiniQueryParameters? skiniQueryParameters, string name);
    Task<IEnumerable<SkiniResponse>> GetSkinesAnaKoinotitaIdInService(SkiniQueryParameters? skiniQueryParameters, int id);
    Task<IEnumerable<SkiniResponse>> GetSkinesEkpaideuomenonInService(SkiniQueryParameters? skiniQueryParameters);
    Task<SkiniResponse> GetSkiniByNameInService(SkiniQueryParameters? skiniQueryParameters, string name);
    Task<SkiniResponse> GetSkiniByIdInService(SkiniQueryParameters? skiniQueryParameters, int id); 

    Task<IEnumerable<KoinotitaResponse>> GetAllKoinotitesInService(KoinotitaQueryParameters? koinotitaQueryParameters);
    Task<IEnumerable<KoinotitaResponse>> GetKoinotitesAnaTomeaInService(KoinotitaQueryParameters? koinotitaQueryParameters, int id);
    Task<KoinotitaResponse> GetKoinotitaByIdInService(int id, KoinotitaQueryParameters koinotitaQueryParameters);
    Task<KoinotitaResponse> GetKoinotitaByNameInService(KoinotitaQueryParameters? koinotitaQueryParameters, string name);

    Task<IEnumerable<TomeasResponse>> GetAllTomeisInService(TomeasQueryParameters tomeasQueryParameters);
    Task<TomeasResponse> GetTomeaByNameInService(TomeasQueryParameters? tomeasQueryParameters, string name);
}

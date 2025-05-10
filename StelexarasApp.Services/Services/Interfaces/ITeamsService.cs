namespace StelexarasApp.Services.Services.IServices;

public interface ITeamsService
{
    Task<bool> HasData();
    Task<bool> AddSkiniInService(CreateSkiniDto skini);
    Task<bool> AddKoinotitaInService(CreateKoinotitaRequest koinotita);
    Task<bool> AddTomeasInService(CreateTomeasRequest tomeas);
    Task<bool> CheckStelexousXwroNameInService(IStelexosDto stelexosDto, string xwrosName);

    Task<bool> DeleteSkiniInService(int skiniId);
    Task<bool> DeleteKoinotitaInService(int koinotitaId);
    Task<bool> DeleteTomeasInService(string n);

    Task<bool> UpdateSkiniInService(int id, UpdateSkiniDto skini);
    Task<bool> UpdateKoinotitaInService(int id, UpdateKoinotitaRequest koinotita);
    Task<bool> UpdateTomeaInService(string id, UpdateTomeasRequest tomeas);

    Task<IEnumerable<SkiniResponse>> GetAllSkinesInService(SkiniQueryParameters? skiniQueryParameters);
    Task<IEnumerable<KoinotitaResponse>> GetAllKoinotitesInService(KoinotitaQueryParameters? koinotitaQueryParameters);
    Task<IEnumerable<TomeasResponse>> GetAllTomeisInService(TomeasQueryParameters tomeasQueryParameters);
    Task<IEnumerable<SkiniResponse>> GetSkinesAnaKoinotitaInService(SkiniQueryParameters? skiniQueryParameters, string name);
    Task<IEnumerable<KoinotitaResponse>> GetKoinotitesAnaTomeaInService(KoinotitaQueryParameters? koinotitaQueryParameters, int name);
    Task<SkiniResponse> GetSkiniByNameInService(SkiniQueryParameters? skiniQueryParameters, string name);
    Task<KoinotitaResponse> GetKoinotitaByNameInService(KoinotitaQueryParameters? koinotitaQueryParameters, string name);
    Task<TomeasResponse> GetTomeaByNameInService(TomeasQueryParameters? tomeasQueryParameters, string name);

    Task<IEnumerable<SkiniResponse>> GetSkinesEkpaideuomenonInService(SkiniQueryParameters? skiniQueryParameters);
}

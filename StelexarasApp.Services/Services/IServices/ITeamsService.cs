using StelexarasApp.Library.QueryParameters;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;

namespace StelexarasApp.Services.Services.IServices;

public interface ITeamsService
{
    Task<bool> HasData();
    Task<bool> AddSkiniInService(SkiniDto skini);
    Task<bool> AddKoinotitaInService(KoinotitaDto koinotita);
    Task<bool> AddTomeasInService(TomeasDto tomeas);
    Task<bool> CheckStelexousXwroNameInService(IStelexosDto stelexosDto, string xwrosName);

    Task<bool> DeleteSkiniInService(int skiniId);
    Task<bool> DeleteKoinotitaInService(int koinotitaId);
    Task<bool> DeleteTomeasInService(string tomeasId);

    Task<bool> UpdateSkiniInService(SkiniDto skini);
    Task<bool> UpdateKoinotitaInService(KoinotitaDto koinotita);
    Task<bool> UpdateTomeaInService(TomeasDto tomeas);

    Task<IEnumerable<SkiniDto>> GetAllSkinesInService(SkiniQueryParameters? skiniQueryParameters);
    Task<IEnumerable<KoinotitaDto>> GetAllKoinotitesInService(KoinotitaQueryParameters? koinotitaQueryParameters);
    Task<IEnumerable<TomeasDto>> GetAllTomeisInService(TomeasQueryParameters tomeasQueryParameters);
    Task<IEnumerable<SkiniDto>> GetSkinesAnaKoinotitaInService(SkiniQueryParameters? skiniQueryParameters, string name);
    Task<IEnumerable<KoinotitaDto>> GetKoinotitesAnaTomeaInService(KoinotitaQueryParameters? koinotitaQueryParameters, int name);
    Task<SkiniDto> GetSkiniByNameInService(SkiniQueryParameters? skiniQueryParameters, string name);
    Task<KoinotitaDto> GetKoinotitaByNameInService(KoinotitaQueryParameters? koinotitaQueryParameters, string name);
    Task<TomeasDto> GetTomeaByNameInService(TomeasQueryParameters? tomeasQueryParameters, string name);

    Task<IEnumerable<SkiniDto>> GetSkinesEkpaideuomenonInService(SkiniQueryParameters? skiniQueryParameters);
}

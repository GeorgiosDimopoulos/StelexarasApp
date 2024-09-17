using StelexarasApp.Services.DtosModels.Domi;

namespace StelexarasApp.Services.IServices;

public interface ITeamsService
{
    Task<bool> AddSkiniInService(SkiniDto skini);
    Task<bool> AddKoinotitaInService(KoinotitaDto skini);
    Task<bool> DeleteSkiniInService(int skiniId);
    Task<bool> DeleteKoinotitaInService(int koinotitaId);
    Task<bool> UpdateSkiniInService(SkiniDto skini);
    Task<IEnumerable<SkiniDto>> GetSkines();
    Task<IEnumerable<SkiniDto>> GetSkinesAnaKoinotitaInService(string name);

    Task<IEnumerable<KoinotitaDto>> GetKoinotitesAnaTomea(int name);

    Task<TomeasDto> GetTomeaByNameInDb(string name); 
    Task<SkiniDto> GetSkiniByName(string name);
}

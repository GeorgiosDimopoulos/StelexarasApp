using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;

namespace StelexarasApp.Services.IServices;

public interface ITeamsService
{
    Task<bool> HasData();
    Task<bool> AddSkiniInService(SkiniDto skini);
    Task<bool> AddKoinotitaInService(KoinotitaDto skini);
    Task<bool> AddTomeasInService(TomeasDto skini);
    Task<bool> CheckStelexousXwroNameInService(StelexosDto stelexosDto, string xwrosName);

    Task<bool> DeleteSkiniInService(int skiniId);
    Task<bool> DeleteKoinotitaInService(int koinotitaId);
    Task<bool> DeleteTomeasInService(int tomeasId);

    Task<bool> UpdateSkiniInService(SkiniDto skini);
    Task<bool> UpdateKoinotitaInService(KoinotitaDto koinotita);
    Task<bool> UpdateTomeaInService(TomeasDto tomeas);

    Task<IEnumerable<SkiniDto>> GetAllSkinesInService();
    Task<IEnumerable<KoinotitaDto>> GetAllKoinotitesInService();
    Task<IEnumerable<TomeasDto>> GetAllTomeisInService();

    Task<IEnumerable<SkiniDto>> GetSkinesAnaKoinotitaInService(string name);
    Task<IEnumerable<KoinotitaDto>> GetKoinotitesAnaTomeaInService(int name);

    Task<SkiniDto> GetSkiniByNameInService(string name);
    Task<SkiniDto> GetKoinotitaByNameInService(string name);
    Task<TomeasDto> GetTomeaByNameInService(string name);

    Task<IEnumerable<SkiniDto>> GetSkinesEkpaideuomenonInService();
}

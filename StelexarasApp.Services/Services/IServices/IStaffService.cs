using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Domi;

namespace StelexarasApp.Services.Services.IServices
{
    public interface IStaffService
    {
        Task<bool> AddStelexosInService(StelexosDto stelexosDto);
        Task<bool> DeleteStelexosByIdInService(int id, Thesi thesi);
        Task<bool> UpdateStelexosInService(StelexosDto stelexosDto);
        Task<StelexosDto> GetStelexosByIdInService(int id, Thesi? thesi);
        Task<StelexosDto> GetStelexosByNameInService(string name, Thesi? thesi);
        Task<IEnumerable<StelexosDto>> GetAllStaffInService();
        Task<IEnumerable<OmadarxisDto>> GetAllOmadarxesInService();
        Task<IEnumerable<KoinotarxisDto>> GetAllKoinotarxesInService();
        Task<IEnumerable<TomearxisDto>> GetAllTomearxesInService();
        Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, string newSkiniName);

        // Task<IEnumerable<StelexosDto>> GetStelexoiAnaXwroInService(Thesi thesi, string? xwrosName);
        Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotitaInService(KoinotitaDto koinotita);
        Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeTomeaInService(TomeasDto tomea);
        Task<IEnumerable<KoinotarxisDto>> GetKoinotarxesSeTomeaInService(TomeasDto tomea);
    }
}

using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;

namespace StelexarasApp.Services.Services.IServices
{
    public interface IStaffService
    {
        Task<bool> AddStelexosInService(IStelexosDto stelexosDto);
        Task<bool> DeleteStelexosByIdInService(int id);
        Task<bool> UpdateStelexosInService(IStelexosDto stelexosDto);
        Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, string newSkiniName);

        Task<IStelexosDto> GetStelexosByIdInService(int id);
        Task<IStelexosDto> GetStelexosByNameInService(string name, Thesi? thesi);
        Task<IEnumerable<IStelexosDto>> GetAllStaffInService();
        Task<IEnumerable<OmadarxisDto>> GetAllOmadarxesInService();
        Task<IEnumerable<KoinotarxisDto>> GetAllKoinotarxesInService();
        Task<IEnumerable<TomearxisDto>> GetAllTomearxesInService();
        Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotitaInService(KoinotitaDto koinotita);
        Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeTomeaInService(TomeasDto tomea);
        Task<IEnumerable<KoinotarxisDto>> GetKoinotarxesSeTomeaInService(TomeasDto tomea);
    }
}

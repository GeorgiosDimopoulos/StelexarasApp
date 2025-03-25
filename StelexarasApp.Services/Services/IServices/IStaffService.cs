using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.QueryParameters;

namespace StelexarasApp.Services.Services.IServices;

public interface IStaffService
{
    Task<bool> AddStelexosInService(IStelexosDto stelexosDto);
    Task<bool> DeleteStelexosByIdInService(int id);
    Task<bool> UpdateStelexosInService(int id, IStelexosDto stelexosDto);
    Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, string newSkiniName);

    Task<IStelexosDto> GetStelexosByIdInService(int id);
    Task<IStelexosDto> GetStelexosByNameInService(string name, Thesi? thesi);
    Task<IEnumerable<IStelexosDto>> GetAllStaffInService(StelexosQueryParameters queryParameters);
    Task<IEnumerable<OmadarxisDto>> GetAllOmadarxesInService(OmadarxisQueryParameters queryParameters);
    Task<IEnumerable<KoinotarxisDto>> GetAllKoinotarxesInService(KoinotarxisQueryParameters queryParameters);
    Task<IEnumerable<TomearxisDto>> GetAllTomearxesInService(TomearxisQueryParameters queryParameters);
    Task<IEnumerable<EkpaideutisDto>> GetAllEkpaideutesInService();
    Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotitaInService(KoinotitaDto koinotita, OmadarxisQueryParameters queryParameters);
    Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeTomeaInService(TomeasDto tomea, OmadarxisQueryParameters queryParameters);
    Task<IEnumerable<KoinotarxisDto>> GetKoinotarxesSeTomeaInService(TomeasDto tomea, KoinotarxisQueryParameters queryParameters);
}

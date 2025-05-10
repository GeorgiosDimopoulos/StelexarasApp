namespace StelexarasApp.Services.Services.IServices.People;

public interface IStaffService
{
    Task<bool> AddStelexosInService(IStelexosDto stelexosDto);
    Task<bool> DeleteStelexosByIdInService(int id);
    Task<bool> UpdateStelexosInService(int id, IStelexosDto stelexosDto);
    Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, string newSkiniName);

    Task<IStelexosDto> GetStelexosByIdInService(int id);
    Task<IStelexosDto> GetStelexosByNameInService(string name, Thesi? thesi);
    Task<IEnumerable<IStelexosDto>> GetAllStaffInService(StelexosQueryParameters queryParameters);
    Task<IEnumerable<OmadarxisResponse>> GetAllOmadarxesInService(OmadarxisQueryParameters queryParameters);
    Task<IEnumerable<KoinotarxisResponse>> GetAllKoinotarxesInService(KoinotarxisQueryParameters queryParameters);
    Task<IEnumerable<TomearxisResponse>> GetAllTomearxesInService(TomearxisQueryParameters queryParameters);
    Task<IEnumerable<EkpaideutisDto>> GetAllEkpaideutesInService();
    Task<IEnumerable<OmadarxisResponse>> GetOmadarxesSeKoinotitaInService(string name, OmadarxisQueryParameters queryParameters);
    Task<IEnumerable<OmadarxisResponse>> GetOmadarxesSeTomeaInService(string tomeaName, OmadarxisQueryParameters queryParameters);
    Task<IEnumerable<KoinotarxisResponse>> GetKoinotarxesSeTomeaInService(string tomeaName, KoinotarxisQueryParameters queryParameters);
}

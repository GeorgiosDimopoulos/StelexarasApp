namespace StelexarasApp.Services.Interfaces.People.Staff;

public interface IKoinotarxisService : IStaffService<CreateKoinotarxisRequest, UpdateKoinotarxisRequest, DeleteKoinotarxisRequest, KoinotarxisResponse>
{
    Task<IEnumerable<KoinotarxisResponse>> GetAllKoinotarxesInService(KoinotarxisQueryParameters queryParameters);
    Task<IEnumerable<KoinotarxisResponse>> GetKoinotarxesSeTomeaInService(string tomeaName, KoinotarxisQueryParameters queryParameters);
    Task<KoinotarxisResponse> GetKoinotarxisByNameInService(string name, KoinotarxisQueryParameters queryParameters);
    Task<KoinotarxisResponse> GetKoinotarxisByIdInService(int id, KoinotarxisQueryParameters queryParameters);
    Task<bool> CreateKoinotarxisInService(CreateKoinotarxisRequest koinotarxisDto);
    Task<bool> UpdateKoinotarxisInService(int id, UpdateKoinotarxisRequest koinotarxisDto);
    Task<bool> DeleteKoinotarxisByIdInService(int id);
}
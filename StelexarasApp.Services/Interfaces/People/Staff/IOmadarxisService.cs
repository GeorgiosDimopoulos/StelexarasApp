namespace StelexarasApp.Services.Interfaces.People.Staff;

public interface IOmadarxisService : IStaffService<CreateOmadarxisRequest, UpdateOmadarxisRequest, DeleteOmadarxisRequest, OmadarxisResponse>
{
    Task<IEnumerable<OmadarxisResponse>> GetAllOmadarxesInService(OmadarxisQueryParameters queryParameters);
    Task<IEnumerable<OmadarxisResponse>> GetOmadarxesSeKoinotitaInService(string name, OmadarxisQueryParameters queryParameters);
    Task<IEnumerable<OmadarxisResponse>> GetOmadarxesSeTomeaInService(string tomeaName, OmadarxisQueryParameters queryParameters);
    Task<OmadarxisResponse> GetOmadarxisByIdInService(int id);
    Task<bool> CreateOmadarxisInService(CreateOmadarxisRequest omadarxisDto);
    Task<bool> UpdateOmadarxisInService(UpdateOmadarxisRequest omadarxisDto);
    Task<bool> DeleteOmadarxisInService(DeleteOmadarxisRequest request);
    Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, string newSkiniName);
}
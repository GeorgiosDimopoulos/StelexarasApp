namespace StelexarasApp.Services.Interfaces.People.Staff;

public interface ITomearxisService : IStaffService<CreateTomearxisRequest, UpdateTomearxisRequest, DeleteTomearxisRequest, TomearxisResponse>
{
    Task<IEnumerable<TomearxisResponse>> GetAllTomearxesInService(TomearxisQueryParameters queryParameters);
    Task<TomearxisResponse> GetTomearxis(string tomeaName, TomearxisQueryParameters queryParameters);
    Task<bool> CreateTomearxisInService(CreateTomearxisRequest request);
    Task<bool> UpdateTomearxisInService(int id, UpdateTomearxisRequest request);
    Task<bool> DeleteTomearxisInService(DeleteTomearxisRequest request);
}
namespace StelexarasApp.Services.Interfaces;

public interface IDutyService
{
    Task<bool> AddDutyInService(CreateDutyRequest req);
    Task<bool> DeleteDutyInService(int id);
    Task<bool> UpdateDutyInService(UpdateDutyRequest req);
    Task<IEnumerable<DutyResponse>> GetDutiesInService();
    Task<DutyResponse> GetDutyByIdInService(int id);
}

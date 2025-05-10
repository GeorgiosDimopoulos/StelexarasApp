using StelexarasApp.Library.Dtos;

namespace StelexarasApp.Services.Services.IServices;

public interface IDutyService
{
    Task<bool> AddDutyInService(CreateDutyRequest duty);
    Task<bool> DeleteDutyInService(int id);
    Task<bool> UpdateDutyInService(string dutyName, UpdateDutyRequest duty);
    Task<IEnumerable<DutyResponse>> GetDutiesInService();
    Task<DutyResponse> GetDutyByIdInService(int id);
}

using StelexarasApp.Library.Dtos;

namespace StelexarasApp.Services.Services.IServices;

public interface IDutyService
{
    Task<bool> AddDutyInService(DutyDto duty);
    Task<bool> DeleteDutyInService(int id);
    Task<bool> UpdateDutyInService(string dutyName, DutyDto duty);
    Task<IEnumerable<DutyDto>> GetDutiesInService();
    Task<DutyDto> GetDutyByIdInService(int id);
}

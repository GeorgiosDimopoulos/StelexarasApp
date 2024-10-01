using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Services.Services.IServices;

public interface IDutyService
{
    Task<bool> AddDutyInService(Duty duty);
    Task<bool> DeleteDutyInService(int id);
    Task<bool> UpdateDutyInService(string dutyName, Duty duty);
    Task<IEnumerable<Duty>> GetDutiesInService();
    Task<bool> HasData();
    Task<Duty> GetDutyByIdInService(int id);
}

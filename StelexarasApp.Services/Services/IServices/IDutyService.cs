using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Services.IServices
{
    public interface IDutyService
    {
        Task<bool> AddDutyInService(Duty duty);
        Task<bool> DeleteDutyInService(string dutyName);
        Task<bool> UpdateDutyInService(string dutyName, Duty duty);
        Task<IEnumerable<Duty>> GetDutiesInService();
    }
}

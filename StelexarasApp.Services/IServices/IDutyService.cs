using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Services.IServices
{
    public interface IDutyService
    {
        Task<bool> AddDutyAsync(Duty duty);
        Task<bool> DeleteDutyAsync(int dutyId);
        Task<bool> UpdateDutyAsync(int dutyId, Duty duty);
        Task<IEnumerable<Duty>> GetDutiesAsync();
    }
}

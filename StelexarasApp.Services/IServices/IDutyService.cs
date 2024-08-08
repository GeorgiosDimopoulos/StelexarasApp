using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Services.IServices
{
    public interface IDutyService
    {
        Task AddDutyAsync(Duty duty);
        Task DeleteDutyAsync(int dutyId);
        Task UpdateDutyAsync(int dutyId, Duty duty);
        Task<IEnumerable<Duty>> GetDutiesAsync();
    }
}

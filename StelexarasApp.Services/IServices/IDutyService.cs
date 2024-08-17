using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Services.IServices
{
    public interface IDutyService
    {
        Task<bool> AddDutyInDbAsync(Duty duty);
        Task<bool> DeleteDutyInDbAsync(string dutyName);
        Task<bool> UpdateDutyInDbAsync(string dutyName, Duty duty);
        Task<IEnumerable<Duty>> GetDutiesFromDbAsync();
    }
}


using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IDutyRepository
    {
        Task<bool> AddDutyAsync(Duty duty);
        Task<bool> DeleteDutyAsync(int id);
        Task<bool> UpdateDutyAsync(int id, Duty newDuty);

        Task<IEnumerable<Duty>> GetDutiesAsync();
    }
}

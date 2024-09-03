
using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IDutyRepository
    {
        Task<bool> AddDutyInDb(Duty duty);
        Task<bool> DeleteDutyInDb(int id);
        Task<bool> UpdateDutyInDb(int id, Duty newDuty);

        Task<Duty> GetDutyFromDb(int dutyInt);
        Task<IEnumerable<Duty>> GetDutiesFromDb();
    }
}

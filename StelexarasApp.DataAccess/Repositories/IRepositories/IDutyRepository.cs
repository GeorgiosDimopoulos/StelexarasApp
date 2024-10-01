
using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IDutyRepository
    {
        Task<bool> AddDutyInDb(Duty duty);
        Task<bool> DeleteDutyInDb(int value);
        Task<bool> UpdateDutyInDb(string name, Duty newDuty);

        Task<Duty> GetDutyFromDb(string name);
        Task<IEnumerable<Duty>> GetDutiesFromDb();
    }
}

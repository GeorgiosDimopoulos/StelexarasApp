namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IDutyRepository
    {
        Task<IEnumerable<Duty>> GetDutiesFromDb();
        Task<Duty> GetDutyFromDb(int id);
        Task<bool> AddDutyInDb(Duty duty);
        Task<bool> DeleteDutyInDb(int id);
        Task<bool> UpdateDutyInDb(Duty duty);
    }
}

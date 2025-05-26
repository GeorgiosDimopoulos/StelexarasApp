namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IDutyRepository
    {
        Task<IEnumerable<Duty>> GetDutiesFromDb();
        Task<Duty> GetDutyFromDb(int id);
        Task<bool> AddDutyInDb(Duty duty);
        Task<bool> DeleteDutyInDb(Duty duty);
        Task<bool> UpdateDutyInDb(Duty duty);
    }
}

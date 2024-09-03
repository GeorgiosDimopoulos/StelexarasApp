using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories
{
    public class DutyRepository(AppDbContext dbContext) : IDutyRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> AddDutyInDb(Duty newDuty)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (newDuty == null || string.IsNullOrEmpty(newDuty?.Name) || _dbContext?.Duties == null)
                {
                    return false;
                }

                _dbContext.Duties.Add(newDuty);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                    await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteDutyInDb(int id)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (_dbContext.Duties is null)
                {
                    return false;
                }

                var existingDuty = await _dbContext.Duties.FindAsync(id);
                if (existingDuty == null)
                {
                    return false;
                }

                _dbContext.Duties?.Remove(existingDuty);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                    await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Duty>> GetDutiesFromDb()
        {
            if (_dbContext.Duties is null)
            {
                return null;
            }

            return await _dbContext.Duties.ToListAsync();
        }

        public async Task<Duty> GetDutyFromDb(int id)
        {
            if (_dbContext.Duties is null)
            {
                return null;
            }

            return await _dbContext.Duties.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> UpdateDutyInDb(int id, Duty newDuty)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (_dbContext.Duties is null)
                {
                    return false;
                }

                var existingDuty = await _dbContext.Duties.FindAsync(id);
                if (existingDuty == null)
                {
                    return false;
                }

                existingDuty.Name = newDuty.Name;
                existingDuty.Date = newDuty.Date;

                if (transaction != null)
                    await transaction.CommitAsync();
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
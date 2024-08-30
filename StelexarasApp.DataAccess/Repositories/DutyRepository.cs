using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories
{
    public class DutyRepository(AppDbContext dbContext) : IDutyRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> AddDutyAsync(Duty newDuty)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (newDuty == null)
                {
                    return false;
                }

                _dbContext.Duties.Add(newDuty);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteDutyAsync(int id)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

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
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Duty>> GetDutiesAsync()
        {
            if (_dbContext.Duties is null)
            {
                return null;
            }

            return await _dbContext.Duties.ToListAsync();
        }

        public async Task<bool> UpdateDutyAsync(int id, Duty newDuty)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

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

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.Library.Models;

namespace StelexarasApp.DataAccess.Repositories
{
    public class DutyRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IDutyRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ILogger<DutyRepository> _logger = loggerFactory.CreateLogger<DutyRepository>();

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
                LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                _logger.LogError("Attempted to AddDutyInDb, exception: " + ex.Message);

                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteDutyInDb(Duty duty)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (_dbContext.Duties is null)
                    return false;

                var existingDuty = await _dbContext.Duties.FirstOrDefaultAsync(d => d.Id == duty.Id);
                if (existingDuty == null)
                    return false;

                _dbContext.Duties?.Remove(existingDuty);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                    await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Duty>> GetDutiesFromDb()
        {
            if (_dbContext.Duties is null)
            {
                return null!;
            }

            return await _dbContext.Duties.ToListAsync();
        }

        public async Task<Duty> GetDutyFromDb(int id)
        {
            if (_dbContext.Duties is null)
                return null!;

            return await _dbContext.Duties.FirstAsync(d => d.Id == id);
        }

        public async Task<bool> UpdateDutyInDb(Duty duty)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (_dbContext.Duties is null)
                {
                    return false;
                }

                var existingDuty = await _dbContext.Duties.FindAsync(duty.Id);
                if (existingDuty == null)
                {
                    return false;
                }

                existingDuty.Name = duty.Name;
                existingDuty.Date = duty.Date;

                if (transaction != null)
                    await transaction.CommitAsync();
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
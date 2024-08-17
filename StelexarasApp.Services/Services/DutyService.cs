using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class DutyService : IDutyService
    {
        private readonly AppDbContext _dbContext;

        public DutyService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddDutyInDbAsync(Duty duty)
        {
            if (string.IsNullOrEmpty(duty.Name))
            {
                throw new ArgumentException("Duty name cannot be null");
            }
            
            _dbContext.Duties?.Add(duty);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDutyInDbAsync(string dutyName)
        {
            var duty = await _dbContext.Duties.FirstOrDefaultAsync(d => d.Name.Equals(dutyName));
            if (duty != null)
            {
                _dbContext.Duties.Remove(duty);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateDutyInDbAsync(string dutyName, Duty updatedDuty)
        {
            var existingDuty = await _dbContext.Duties.FirstOrDefaultAsync(d => d.Name.Equals(dutyName));
            if (existingDuty != null)
            {
                existingDuty.Name = updatedDuty.Name;
                existingDuty.Date = DateTime.Now;
                
                _dbContext.Duties.Update(existingDuty);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Duty>> GetDutiesFromDbAsync()
        {
            return await _dbContext.Duties.ToListAsync();
        }
    }
}

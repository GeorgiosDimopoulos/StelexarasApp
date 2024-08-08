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

        public async Task AddDutyAsync(Duty duty)
        {

            _dbContext.Duties?.Add(duty);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteDutyAsync(int dutyId)
        {
            var duty = await _dbContext.Duties.FindAsync(dutyId);
            if (duty != null)
            {
                _dbContext.Duties.Remove(duty);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateDutyAsync(int dutyId, Duty updatedDuty)
        {
            var existingDuty = await _dbContext.Duties.FindAsync(dutyId);
            if (existingDuty != null)
            {
                existingDuty.Name = updatedDuty.Name;
                existingDuty.Date = DateTime.Now;
                _dbContext.Duties.Update(existingDuty);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Duty>> GetDutiesAsync()
        {
            return await _dbContext.Duties.ToListAsync();
        }
    }
}

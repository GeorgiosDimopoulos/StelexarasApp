using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class KoinotitaService : IKoinotitaService
    {
        private readonly AppDbContext _dbContext;

        public KoinotitaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPaidiAsync(Kataskinotis paidi)
        {

            _dbContext.Kataskinotes?.Add(paidi);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeletePaidiAsync(int paidiId)
        {
            var paidi = await _dbContext.Kataskinotes.FindAsync(paidiId);
            if (paidi != null)
            {
                _dbContext.Kataskinotes.Remove(paidi);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdatePaidiAsync(int paidiId)
        {
            // 
        }

        public async Task<IEnumerable<Kataskinotis>> GetPaidiaAsync()
        {
            return await _dbContext.Kataskinotes.ToListAsync();
        }
    }
}

using Azure;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly AppDbContext _dbContext;

        public PeopleService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddPaidiInDbAsync(Paidi paidi, string skiniName)
        {
            if (paidi is null) return false;

            var skini = await _dbContext.Skines.FirstOrDefaultAsync(s => s.Name == skiniName);
            if (skini == null)
                return false;
            else
                paidi.Skini = skini;

            if (paidi.PaidiType == PaidiType.Ekpaideuomenos)
            {
                _dbContext.Ekpaideuomenoi.Add((Ekpaideuomenos)paidi);
            }
            else if (paidi.PaidiType == PaidiType.Kataskinotis)
            {
                _dbContext.Kataskinotes.Add((Kataskinotis)paidi);
            }
            
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePaidiInDbAsync(Paidi paidi)
        {
            if(paidi is null) return false;

            if (paidi.PaidiType == PaidiType.Ekpaideuomenos)
            {
                var kataskinotis = await _dbContext.Kataskinotes.FindAsync(paidi.FullName);
                _dbContext.Kataskinotes.Remove(kataskinotis);
            }
            else if (paidi.PaidiType == PaidiType.Kataskinotis)
            {
                var ekpaideuomenos = await _dbContext.Ekpaideuomenoi.FindAsync(paidi.FullName);
                _dbContext.Ekpaideuomenoi.Remove(ekpaideuomenos);
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdatePaidiInDbAsync(Paidi paidi, string skiniName)
        {
            if (paidi is null) return false;
            return true;
        }

        public async Task<IEnumerable<Skini>> GetSkinesAsync()
        {
            return await _dbContext.Skines
                .Include(s => s.Paidia)
                .Include(s => s.Koinotita)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paidi>> GetEkpaideuomenoiAsync()
        {
            return await _dbContext.Ekpaideuomenoi.ToListAsync();
        }

        public async Task<IEnumerable<Paidi>> GetKataskinotesAsync()
        {
            return await _dbContext.Ekpaideuomenoi.ToListAsync();
        }
    }
}

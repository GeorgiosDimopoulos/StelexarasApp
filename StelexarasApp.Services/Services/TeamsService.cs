using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly AppDbContext _dbContext;

        public TeamsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddSkinesInDbAsync(Skini skini)
        {
            if (skini is null || string.IsNullOrEmpty(skini.Name))
            {
                return false;
            }

            _dbContext.Skines.Add(skini);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPaidiInDbAsync(Paidi paidi)
        {
            if (paidi is null || string.IsNullOrEmpty(paidi.FullName))
            {
                return false;
            }

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
            if (paidi is null) return false;

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

        public async Task<bool> UpdatePaidiInDbAsync(Paidi paidi)
        {
            if (paidi is null)
                return false;

            if (paidi.PaidiType == PaidiType.Ekpaideuomenos)
                {
                _dbContext.Ekpaideuomenoi.Update((Ekpaideuomenos)paidi);
            }
            else if (paidi.PaidiType == PaidiType.Kataskinotis)
            {
                _dbContext.Kataskinotes.Update((Kataskinotis)paidi);
            }
            return true;
        }

        public async Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId)
        {
            var paidi = await _dbContext.Kataskinotes
                .Include(p => p.Skini)
                .FirstOrDefaultAsync(p => p.Id == paidiId);

            if (paidi == null)
            {
                return false;
            }

            var oldSkini = paidi.Skini;
            var newSkini = await _dbContext.Skines
                .Include(s => s.Paidia)
                .FirstOrDefaultAsync(s => s.Id == newSkiniId);

            if (newSkini == null)
            {
                return false;
            }

            if (oldSkini != null)
            {
                oldSkini.Paidia.Remove(paidi);
            }

            newSkini.Paidia.Add(paidi);
            paidi.Skini = newSkini;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Paidi> GetPaidiByIdAsync(int id, PaidiType type)
        {
            if (type == PaidiType.Ekpaideuomenos)
            {
                return await _dbContext.Ekpaideuomenoi.FirstAsync(p => p.Id == id);
            }

            return await _dbContext.Kataskinotes.FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Skini>> GetSkinesAsync()
        {
            return await _dbContext.Skines
                .Include(s => s.Paidia)
                .Include(s => s.Koinotita)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paidi>> GetPaidiaAsync(PaidiType type)
        {
            if (type == PaidiType.Ekpaideuomenos)
            {
                return await _dbContext.Ekpaideuomenoi.ToListAsync();
            }

            return await _dbContext.Ekpaideuomenoi.ToListAsync();
        }

        public Task<Skini> GetSkiniByNameAsync(string name)
        {
            return _dbContext.Skines?.FirstOrDefaultAsync(s => s.Name.Equals(name));
        }
    }
}
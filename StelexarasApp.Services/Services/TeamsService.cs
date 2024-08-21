using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma;
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

        public async Task<bool> AddSkinesInDb(Skini skini)
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
            if (paidi is null || paidi.Id <= 0)
            {
                return false;
            }

            _dbContext.Paidia.Add(paidi);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePaidiInDb(Paidi paidi)
        {
            if (paidi == null || paidi.Id <= 0)
                return false;

            var existingPaidi = await _dbContext.Paidia.FindAsync(paidi.Id);
            if (existingPaidi != null)
            {
                _dbContext.Paidia.Remove(existingPaidi);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePaidiInDb(Paidi paidi)
        {
            if (paidi is null)
                return false;

            _dbContext.Paidia.Update(paidi);

            return true;
        }

        //public async Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId)
        //{
        //    var paidi = await _dbContext.Paidia
        //        .Include(p => p.Skini)
        //        .FirstOrDefaultAsync(p => p.Id == paidiId);

        //    if (paidi == null)
        //    {
        //        return false;
        //    }

        //    var oldSkini = paidi.Skini;

        //    var newSkini = await _dbContext.Skines
        //        .Include(s => s.Paidia)
        //        .FirstOrDefaultAsync(s => s.Id == newSkiniId);

        //    if (newSkini == null)
        //    {
        //        return false;
        //    }

        //    if (oldSkini != null && oldSkini.Paidia != null)
        //    {
        //        oldSkini.Paidia.Remove(paidi);
        //    }

        //    newSkini.Paidia.Add(paidi);
        //    paidi.Skini = newSkini;

        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return false;
        //    }
        //}

        public async Task<Paidi> GetPaidiById(int id, PaidiType type)
        {
            return await _dbContext.Paidia.FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Skini>> GetSkines()
        {
            return await _dbContext.Skines
                .Include(s => s.Paidia)
                .Include(s => s.Koinotita)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paidi>> GetPaidia(PaidiType type)
        {
            return await _dbContext.Paidia.ToListAsync();
        }

        public Task<Skini> GetSkiniByName(string name)
        {
            return _dbContext.Skines?.FirstOrDefaultAsync(s => s.Name.Equals(name));
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.Library.Models.Domi;
using StelexarasApp.Library.Models.Logs;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Library.QueryParameters;

namespace StelexarasApp.DataAccess.Repositories
{
    public class TeamsRepository(AppDbContext appDbContext, ILoggerFactory loggerFactory) : ITeamsRepository
    {
        private readonly AppDbContext _dbContext = appDbContext;
        private readonly ILogger<TeamsRepository> _logger = loggerFactory.CreateLogger<TeamsRepository>();

        public async Task<IEnumerable<Koinotita>> GetKoinotitesInDb(KoinotitaQueryParameters? koinotitaQueryParameters)
        {
            try
            {
                var query = _dbContext.Koinotites!.AsQueryable();
                if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeSkines)
                {
                    query = query.Include(k => k.Skines);
                }
                if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeOmadarxes)
                {
                    query = query.Include(k => k.Skines)!.ThenInclude(s => s.Omadarxis);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                return null!;
            }
        }

        public async Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomeaInDb(KoinotitaQueryParameters? koinotitaQueryParameters, int tomeaId)
        {
            try
            {
                var query = _dbContext.Koinotites!.AsQueryable();
                if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeSkines)
                {
                    query = query.Include(k => k.Skines);
                }
                if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeOmadarxes)
                {
                    query = query.Include(k => k.Skines)!.ThenInclude(s => s.Omadarxis);
                }
                return await _dbContext.Koinotites!.Include(k => k.Tomeas).Where(k => k.Tomeas.Id == tomeaId).ToListAsync();
            }
            catch (Exception ex)
            {
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkinesInDb(SkiniQueryParameters? skiniQueryParameters)
        {
            try
            {
                var query = _dbContext.Skines!.AsQueryable();
                if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
                {
                    query = query.Include(s => s.Paidia);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaInDb(SkiniQueryParameters? skiniQueryParameters, string koinotitaName)
        {
            try
            {
                var query = _dbContext.Skines!.Where(sk => sk.Koinotita.Name.Equals(koinotitaName)).AsQueryable();
                if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
                {
                    query = query.Include(s => s.Paidia);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkinesEkpaideuomenonInDb(SkiniQueryParameters? skiniQueryParameters)
        {
            try
            {
                var query = _dbContext.Skines.Where(s => s.Koinotita.Name == "Ipiros").AsQueryable();
                if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
                {
                    query = query.Include(s => s.Paidia);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return null!;
            }
        }

        public async Task<Skini> GetSkiniByNameInDb(SkiniQueryParameters? skiniQueryParameters, string name)
        {
            var query = _dbContext.Skines.AsQueryable();

            if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
            {
                query = query.Include(s => s.Paidia);
            }

            return query.FirstOrDefault(s => s.Name == name) ?? new Skini();
        }

        public async Task<IEnumerable<Tomeas>> GetTomeisInDb(TomeasQueryParameters? tomeasQueryParameters)
        {
            try
            {
                var query = _dbContext.Tomeis!.AsQueryable();
                if (tomeasQueryParameters is not null && tomeasQueryParameters.IncludeKoinotites)
                {
                    query = query.Include(t => t.Koinotites);
                }
                if (tomeasQueryParameters.IncludeOmadarxes)
                {
                    query = query.Include(t => t.Koinotites).ThenInclude(k => k.Skines)!.ThenInclude(sk => sk.Omadarxis);
                }
                if (tomeasQueryParameters.IncludeKoinotarxes)
                {
                    query = query.Include(t => t.Koinotites).ThenInclude(k => k.Koinotarxis);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return null!;
            }
        }

        public async Task<Koinotita> GetKoinotitaByNameInDb(KoinotitaQueryParameters? koinotitaQueryParameters, string name)
        {
            var query = _dbContext.Koinotites!.AsQueryable();
            if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeSkines)
            {
                query = query.Include(k => k.Skines);
            }
            return await query.FirstOrDefaultAsync(k => k.Name == name) ?? new Koinotita();
        }

        public async Task<Tomeas> GetTomeaByNameInDb(TomeasQueryParameters? tomeasQueryParameters, string name)
        {
            var query = _dbContext.Tomeis!.AsQueryable();
            if (tomeasQueryParameters.IncludeKoinotites)
            {
                query = query.Include(t => t.Koinotites);
            }
            if (tomeasQueryParameters.IncludeKoinotarxes)
            {
                query = query.Include(t => t.Koinotites).ThenInclude(k => k.Koinotarxis);
            }
            if (tomeasQueryParameters.IncludeOmadarxes)
            {
                query = query.Include(t => t.Koinotites).ThenInclude(k => k.Skines)!.ThenInclude(sk => sk.Omadarxis);
            }
            if (string.IsNullOrEmpty(name) || _dbContext.Tomeis is null)
                return null!;

            return await query.FirstOrDefaultAsync(t => t.Name == name) ?? new Tomeas();
        }

        public async Task<bool> UpdateKoinotitaInDb(Koinotita koinotita)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (koinotita is null || _dbContext.Koinotites is null)
                return false;
            try
            {
                _dbContext.Koinotites.Update(koinotita);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public async Task<bool> UpdateSkiniInDb(Skini skini)
        {

            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (skini is null || _dbContext.Skines is null)
                return false;

            try
            {
                _dbContext.Skines.Update(skini);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public async Task<bool> UpdateTomeasInDb(Tomeas tomeas)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (tomeas is null || _dbContext.Tomeis is null)
                return false;

            try
            {
                _dbContext.Tomeis.Update(tomeas);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public async Task<bool> DeleteKoinotitaInDb(int koinotitaId)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (koinotitaId == 0 || _dbContext.Koinotites is null)
                return false;

            try
            {
                var koinotita = await _dbContext.Koinotites.FindAsync(koinotitaId);
                if (koinotita == null)
                    return false;

                _dbContext.Koinotites.Remove(koinotita);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public async Task<bool> DeleteSkiniInDb(int skiniId)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (skiniId <= 0 || _dbContext.Skines is null)
                    return false;

                var skini = await _dbContext.Skines.FindAsync(skiniId);
                if (skini == null)
                    return false;

                _dbContext.Skines.Remove(skini);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public async Task<bool> DeleteTomeasInDb(string name)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (string.IsNullOrEmpty(name) || _dbContext.Tomeis is null)
                    return false;

                var tomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(t => t.Name.Equals(name));
                if (tomeas == null)
                    return false;

                _dbContext.Tomeis.Remove(tomeas);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                    await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public async Task<bool> AddSkiniInDb(Skini skini)
        {
            if (skini == null || skini.Koinotita == null || skini.Koinotita.Tomeas == null || skini.Id <= 0 || (await _dbContext.Skines.FirstOrDefaultAsync(s => s.Name == skini.Name)) is not null || _dbContext.Skines is null)
                return false;

            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var existingTomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(t => t.Name == skini.Koinotita.Tomeas.Name);
                if (existingTomeas != null)
                    skini.Koinotita.Tomeas = existingTomeas;

                var existingKoinotita = await _dbContext.Koinotites.FirstOrDefaultAsync(t => t.Name == skini.Koinotita.Name);
                if (existingKoinotita != null)
                    skini.Koinotita = existingKoinotita;

                await _dbContext.Skines.AddAsync(skini);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                    await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public async Task<bool> AddKoinotitaInDb(Koinotita koinotita)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (koinotita is null || _dbContext.Koinotites is null || (await _dbContext.Koinotites.FirstOrDefaultAsync(s => s.Name == koinotita.Name)) is not null)
                    return false;

                await _dbContext.Koinotites.AddAsync(koinotita);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                    await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public async Task<bool> AddTomeasInDb(Tomeas tomeas)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if ((await _dbContext.Tomeis.FirstOrDefaultAsync(s => s.Name == tomeas.Name)) is not null || tomeas is null || _dbContext.Tomeis is null)
                    return false;

                var existingTomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(k => k.Name == tomeas.Name);
                if (existingTomeas != null)
                    return false;

                await _dbContext.Tomeis.AddAsync(tomeas);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                    await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();
                ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
                return false;
            }
        }

        public Task<bool> HasData()
        {
            if (_dbContext is null)
                return Task.FromResult(false);

            if (!GetSkinesInDb(new()).Result.Any() &&
                    !GetKoinotitesAnaTomeaInDb(new(), 2).Result.Any() &&
                    !GetKoinotitesAnaTomeaInDb(new(), 1).Result.Any())
                return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}

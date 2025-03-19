using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Domi;
using StelexarasApp.Library.Models.Logs;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories;

public class StaffRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IStaffRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<StaffRepository> _logger = loggerFactory.CreateLogger<StaffRepository>();

    public async Task<bool> AddOmadarxiInDb(Omadarxis omadarxis)
    {
        if (omadarxis == null || omadarxis.Id <= 0 || omadarxis.Id > 100 || (int)omadarxis.Thesi > 4)
            throw new ArgumentException(nameof(omadarxis), "Omadarxis cannot be null or its id negative or huge value!");
        if (_dbContext.Omadarxes == null)
            throw new InvalidOperationException("Omadarxes DbSet cannot be null");

        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (await _dbContext.Omadarxes.AnyAsync(s => s.Tel == omadarxis.Tel))
            return false;
        try
        {
            if (omadarxis.Skini == null || omadarxis.Skini.Koinotita == null || omadarxis.Skini.Koinotita.Tomeas == null)
                throw new ArgumentException("Skini, Koinotita, and Tomeas must not be null.");

            if (omadarxis.Thesi != Thesi.Omadarxis)
                throw new InvalidOperationException($"Invalid Thesi for Tomearxis. Expected {Thesi.Omadarxis}, but got {omadarxis.Thesi}.");

            var existingSkini = await _dbContext.Skines.FirstOrDefaultAsync(k => k.Name == omadarxis.Skini.Name);
            if (existingSkini != null)
                omadarxis.Skini = existingSkini;

            var existingTomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(t => t.Name == omadarxis.Skini.Koinotita.Tomeas.Name);
            if (existingTomeas != null)
                omadarxis.Skini.Koinotita.Tomeas = existingTomeas;

            _dbContext.Omadarxes!.Add(omadarxis);
            await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> AddKoinotarxiInDb(Koinotarxis koinotarxis)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if ((await _dbContext.Koinotarxes.FirstOrDefaultAsync(s => s.Tel == koinotarxis.Tel)) is not null || koinotarxis == null || _dbContext.Omadarxes is null)
            return false;

        try
        {
            if (koinotarxis.Thesi != Thesi.Koinotarxis)
                throw new InvalidOperationException($"Invalid Thesi for Tomearxis. Expected {Thesi.Koinotarxis}, but got {koinotarxis.Thesi}.");

            var existingKoinotita = await _dbContext.Koinotites.FirstOrDefaultAsync(k => k.Name == koinotarxis.Koinotita.Name);
            if (existingKoinotita != null)
                koinotarxis.Koinotita = existingKoinotita;

            _dbContext.Koinotarxes!.Add(koinotarxis);
            await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();
            return true;
        }
        catch (DbUpdateException dbEx) when (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
        {
            _logger.LogError(dbEx, "{MethodName}, exception: {ExceptionMessage}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, dbEx.Message);
            LogFileWriter.WriteToLog($"{dbEx.Message}, {dbEx.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            if (transaction != null)
                await transaction.RollbackAsync();
            return false;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> AddTomearxiInDb(Tomearxis tomearxis)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if ((await _dbContext.Tomearxes.FirstOrDefaultAsync(s => s.Tel == tomearxis.Tel)) is not null || tomearxis == null || _dbContext.Omadarxes is null)
            return false;

        if (tomearxis.Thesi != Thesi.Tomearxis)
            throw new InvalidOperationException($"Invalid Thesi for Tomearxis. Expected {Thesi.Omadarxis}, but got {tomearxis.Thesi}.");

        var existingTomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(t => t.Name.Equals(tomearxis.Tomeas.Name));
        if (existingTomeas != null)
            tomearxis.Tomeas = existingTomeas;
        try
        {
            _dbContext.Tomearxes!.Add(tomearxis);
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

    public async Task<bool> DeleteStelexosInDb(int id)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var omadarxis = await _dbContext.Omadarxes.FindAsync(id);
            if (omadarxis != null)
                _dbContext.Omadarxes.Remove(omadarxis);
            var koinotarxis = await _dbContext.Koinotarxes.FindAsync(id);
            if (koinotarxis != null)
                _dbContext.Koinotarxes.Remove(koinotarxis);
            var tomearxis = await _dbContext.Tomearxes.FindAsync(id);
            if (tomearxis != null)
                _dbContext.Tomearxes.Remove(tomearxis);

            var changes = await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();
            return changes > 0;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<IEnumerable<Omadarxis>> GetOmadarxesSeKoinotitaInDb(Koinotita koinotita)
    {
        return (IEnumerable<Omadarxis>)await GetStelexoiAnaXwroInDb(Thesi.Omadarxis, koinotita.Name);
    }

    public async Task<IEnumerable<IStelexos>> GetStelexoiAnaXwroInDb(Thesi thesi, string? xwrosName)
    {
        try
        {
            return thesi switch
            {
                Thesi.Omadarxis => xwrosName != null ? await GetOmadarxesAnaXwro(xwrosName) : [],
                Thesi.Koinotarxis => xwrosName != null ? await GetKoinotarxesAnaXwro(xwrosName) : [],
                Thesi.Tomearxis => await _dbContext.Tomearxes!.ToListAsync(),
                Thesi.None => throw new NotImplementedException(),
                Thesi.Ekpaideutis => await _dbContext.Ekpaideutes!.ToListAsync(),
                _ => throw new NotImplementedException(),
            };
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return [];
        }
    }

    public async Task<IStelexos> GetStelexosByIdInDb(int id)
    {
        if (_dbContext is null)
            return null!;

        var omadarxis = await _dbContext.Omadarxes!.FirstOrDefaultAsync(o => o.Id == id);
        if (omadarxis != null)
            return omadarxis;
        var koinotarxis = await _dbContext.Koinotarxes!.FirstOrDefaultAsync(k => k.Id == id);
        if (koinotarxis != null)
            return koinotarxis;
        var tomearxis = await _dbContext.Tomearxes!.FirstOrDefaultAsync(t => t.Id == id);
        if (tomearxis != null)
            return tomearxis;
        var ekpaideutis = await _dbContext.Ekpaideutes!.FirstOrDefaultAsync(e => e.Id == id);
        if (ekpaideutis != null)
            return ekpaideutis;

        //switch (thesi)
        //{
        //    case Thesi.None:
        //        break;
        //    case Thesi.Omadarxis:
        //        if (_dbContext.Omadarxes == null)
        //            return null!;
        //        return await _dbContext.Omadarxes!.FirstOrDefaultAsync(om => om.Id == id) ?? null!;
        //    case Thesi.Koinotarxis:
        //        if (_dbContext.Koinotarxes == null)
        //            return null!;
        //        return await _dbContext.Koinotarxes.FirstOrDefaultAsync(ko => ko.Id == id) ?? null!;
        //    case Thesi.Tomearxis:
        //        if (_dbContext.Tomearxes == null)
        //            return null!;
        //        return await _dbContext.Tomearxes.FirstOrDefaultAsync(to => to.Id == id) ?? null!;
        //    case Thesi.Ekpaideutis:
        //        throw new NotImplementedException();
        //    default:
        //        break;
        //}
        return null!;
    }

    public async Task<bool> UpdateStelexosInDb(IStelexos stelexos)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        var parts = stelexos.FullName.Trim().Split(' ');
        if (parts.Length < 2)
            throw new ArgumentException("Invalid FullName", nameof(stelexos.FullName));

        try
        {
            switch (stelexos.Thesi)
            {
                case Thesi.Omadarxis:
                    if (_dbContext.Omadarxes == null)
                        return false;
                    _dbContext.Omadarxes.Update((Omadarxis)stelexos);
                    break;
                case Thesi.Koinotarxis:
                    if (_dbContext.Koinotarxes == null)
                        return false;
                    _dbContext.Koinotarxes.Update((Koinotarxis)stelexos);
                    break;
                case Thesi.Tomearxis:
                    if (_dbContext.Tomearxes == null)
                        return false;
                    _dbContext.Tomearxes.Update((Tomearxis)stelexos);
                    break;
                case Thesi.Ekpaideutis:
                    if (_dbContext.Ekpaideutes == null)
                        return false;
                    _dbContext.Ekpaideutes.Update((Ekpaideutis)stelexos);
                    break;
                case Thesi.None:
                    break;
                default:
                    throw new ArgumentException("Invalid Thesi value!", nameof(stelexos.Thesi));
            }

            var changes = await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync(); return changes > 0;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> MoveOmadarxisToAnotherSkiniInDb(int id, string newSkiniName)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (_dbContext.Skines is null || _dbContext.Omadarxes is null)
            return false;

        try
        {
            var omadarxisInDb = await _dbContext.Omadarxes.Include(o => o.Skini).FirstOrDefaultAsync(o => o.Id == id);
            var newSkini = await _dbContext.Skines.FirstOrDefaultAsync(s => s.Name.Equals(newSkiniName));

            if (newSkini == null || newSkini.Omadarxis == omadarxisInDb || omadarxisInDb == null)
                return false;

            var oldSkini = omadarxisInDb.Skini;
            if (oldSkini != null)
            {
                newSkini.Omadarxis = omadarxisInDb;
                oldSkini.Omadarxis = null!;
                omadarxisInDb.Skini = newSkini;
            }

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

    public async Task<IStelexos> GetStelexosByNameInDb(string name, Thesi? thesi)
    {
        try
        {
            if (string.IsNullOrEmpty(name) || thesi is null)
                return null!;

            if (thesi == Thesi.None)
            {
                IQueryable<IStelexos> query = _dbContext.Omadarxes!.Cast<IStelexos>()
                    .Concat(_dbContext.Koinotarxes!.Cast<IStelexos>())
                    .Concat(_dbContext.Tomearxes!.Cast<IStelexos>());
                return await query.FirstOrDefaultAsync(e => e.FullName == name) ?? null!;
            }

            return thesi switch
            {
                Thesi.Omadarxis => await _dbContext.Omadarxes!.FirstOrDefaultAsync(o => o.FullName == name) ?? null!,
                Thesi.Koinotarxis => await _dbContext.Koinotarxes!.FirstOrDefaultAsync(k => k.FullName == name) ?? null!,
                Thesi.Tomearxis => await _dbContext.Tomearxes!.FirstOrDefaultAsync(t => t.FullName == name) ?? null!,
                Thesi.Ekpaideutis => await _dbContext.Ekpaideutes!.FirstOrDefaultAsync(t => t.FullName == name) ?? null!,
                _ => throw new ArgumentOutOfRangeException(nameof(thesi), thesi, null)
            };
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    private async Task<IEnumerable<Omadarxis>> GetOmadarxesAnaXwro(string xwrosName)
    {
        if (string.IsNullOrEmpty(xwrosName))
            return await _dbContext.Omadarxes.ToListAsync();

        var isXwrosAnKoinotita = await _dbContext.Koinotites!.AnyAsync(k => k.Name.Equals(xwrosName));
        if (isXwrosAnKoinotita)
            return await GetOmadarxesAnaKoinotita(xwrosName);
        else
        {
            var isXwrosATomeas = _dbContext.Tomeis!.Any(t => t.Name.Equals(xwrosName));
            if (isXwrosATomeas)
                return await GetOmadarxesAnaTomea(xwrosName);
        }

        return await _dbContext.Omadarxes.ToListAsync();
    }

    private async Task<IEnumerable<Koinotarxis>> GetKoinotarxesAnaXwro(string xwrosName)
    {
        if (_dbContext.Koinotarxes == null | !_dbContext.Koinotarxes!.Any())
            return null!;
        if (string.IsNullOrEmpty(xwrosName))
        {
            var allKoinotarxes = await _dbContext.Koinotarxes!.ToListAsync();
            return allKoinotarxes;
        }

        var isXwrosAnTomeas = _dbContext.Tomeis!.Any(k => k.Name.Equals(xwrosName));
        if (isXwrosAnTomeas)
            return await GetKoinotarxesAnaTomea(xwrosName);
        else
            return null!;
    }

    private async Task<List<Omadarxis>> GetOmadarxesAnaKoinotita(string? name)
    {
        if (string.IsNullOrEmpty(name))
            return await _dbContext.Omadarxes!.ToListAsync();
        return await _dbContext.Skines!.Where(k => k.Koinotita.Name == name).Select(k => k.Omadarxis!).ToListAsync();
    }

    private async Task<List<Omadarxis>> GetOmadarxesAnaTomea(string? name)
    {
        if (string.IsNullOrEmpty(name))
            return await _dbContext.Omadarxes!.ToListAsync();
        return await _dbContext.Skines!.Where(sk => sk.Koinotita.Tomeas.Name == name).Select(k => k.Omadarxis!).ToListAsync();
    }

    private async Task<List<Koinotarxis>> GetKoinotarxesAnaTomea(string? tomeasName)
    {
        if (string.IsNullOrEmpty(tomeasName))
            return await _dbContext.Koinotarxes!.ToListAsync();

        return await _dbContext.Koinotites!.Where(k => k.Tomeas!.Name == tomeasName).Select(k => k.Koinotarxis!).ToListAsync();
    }

    public async Task<bool> AddStelexosInDb(IStelexos stelexos)
    {
        switch (stelexos.Thesi)
        {
            case Thesi.None:
                break;
            case Thesi.Omadarxis:
                await _dbContext.Omadarxes.AddAsync((Omadarxis)stelexos);
                break;
            case Thesi.Koinotarxis:
                await _dbContext.Koinotarxes.AddAsync((Koinotarxis)stelexos);
                break;
            case Thesi.Tomearxis:
                await _dbContext.Tomearxes.AddAsync((Tomearxis)stelexos);
                break;
            case Thesi.Ekpaideutis:
                await _dbContext.Ekpaideutes.AddAsync((Ekpaideutis)stelexos);
                break;
            default:
                break;
        }

        await _dbContext.SaveChangesAsync();

        return true;
    }
}

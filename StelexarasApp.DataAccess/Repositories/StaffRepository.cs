using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories;

public class StaffRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IStaffRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<StaffRepository> _logger = loggerFactory.CreateLogger<StaffRepository>();

    public async Task<bool> AddStelexosInDb(Stelexos stelexos)
    {
        var isInMemoryDatabase = _dbContext.Database?.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase || _dbContext.Database == null
            ? null 
            : await _dbContext.Database.BeginTransactionAsync();

        if (stelexos == null)
            throw new ArgumentNullException(nameof(stelexos), "Stelexos cannot be null");

        try
        {
            var parts = stelexos.FullName.Trim().Split(' ');
            if (parts.Length < 2)
                throw new ArgumentException("Invalid FullName", nameof(stelexos.FullName));

            switch (stelexos.Thesi)
            {
                case Thesi.Omadarxis:
                    _dbContext.Omadarxes?.Add((Omadarxis)stelexos);
                    break;
                case Thesi.Koinotarxis:
                    _dbContext.Koinotarxes?.Add((Koinotarxis)stelexos);
                    break;
                case Thesi.Tomearxis:
                    _dbContext.Tomearxes?.Add((Tomearxis)stelexos);
                    break;
                case Thesi.Ekpaideutis:
                    throw new NullReferenceException("Not yet implemented!");
                case Thesi.None:
                    throw new ArgumentException("Thesi cannot be None!", nameof(stelexos.Thesi));
                default:
                    throw new ArgumentException("Invalid Thesi value!", nameof(stelexos.Thesi));
            }

            await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();

            return true;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError($"Database update exception: {ex.Message}");
            _dbContext.Dispose();
            throw new DbUpdateException("Invalid Thesi cast.", ex);
        }
        catch (InvalidCastException ex)
        {
            _logger.LogError($"Invalid cast for Stelexos of Thesi {stelexos.Thesi}: {ex.Message}");
            _dbContext.Dispose();
            throw new InvalidCastException("Invalid Thesi cast.", ex);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError($"Invalid cast for Stelexos of Thesi {stelexos.Thesi}: {ex.Message}");
            _dbContext.Dispose();
            throw new ArgumentException("ArgumentException: ", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
            if (transaction != null)
                await transaction.RollbackAsync();
            _dbContext.Dispose();
            return false;
        }
        //finally
        //{
        //    _dbContext.Dispose(); 
        //}
    }

    public async Task<bool> DeleteStelexosInDb(int id, Thesi thesi)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            switch (thesi)
            {
                case Thesi.Omadarxis:
                    var omadarxis = await _dbContext.Omadarxes.FindAsync(id);
                    if (omadarxis != null)
                        _dbContext.Omadarxes.Remove(omadarxis);
                    break;
                case Thesi.Koinotarxis:
                    var koinotarxis = await _dbContext.Koinotarxes.FindAsync(id);
                    if (koinotarxis != null)
                        _dbContext.Koinotarxes.Remove(koinotarxis);
                    break;
                case Thesi.Tomearxis:
                    var tomearxis = await _dbContext.Tomearxes.FindAsync(id);
                    if (tomearxis != null)
                        _dbContext.Tomearxes.Remove(tomearxis);
                    break;
                default:
                    throw new ArgumentException("Invalid Thesi value", nameof(thesi));
            }

            var changes = await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();
            return changes > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
            LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
            return false;
        }
    }

    public async Task<IEnumerable<Omadarxis>> GetOmadarxesSeKoinotitaInDb(Koinotita koinotita)
    {
        return (IEnumerable<Omadarxis>)await GetStelexoiAnaXwroInDb(Thesi.Omadarxis, koinotita.Name);
    }

    public async Task<IEnumerable<Stelexos>> GetStelexoiAnaXwroInDb(Thesi thesi, string? xwrosName)
    {
        try
        {
            return thesi switch
            {
                Thesi.Omadarxis => await GetOmadarxesAnaXwro(xwrosName),
                Thesi.Koinotarxis => await GetKoinotarxesAnaXwro(xwrosName),
                Thesi.Tomearxis => await _dbContext.Tomearxes!.ToListAsync(),
                Thesi.None => throw new NotImplementedException(),
                Thesi.Ekpaideutis => await _dbContext.Ekpaideutes!.ToListAsync(),
                _ => throw new NotImplementedException(),
            };
        }
        catch (Exception ex)
        {
           _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
            LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
            return null!;
        }
    }

    public async Task<Stelexos> GetStelexosByIdInDb(int id, Thesi? thesi)
    {
        if (thesi == Thesi.None || id <= 0 || _dbContext is null)
            return null!;

        switch (thesi)
        {
            case Thesi.None:
                break;
            case Thesi.Omadarxis:
                if (_dbContext.Omadarxes == null)
                    return null!;
                return await _dbContext.Omadarxes!.FirstOrDefaultAsync(om => om.Id == id);
            case Thesi.Koinotarxis:
                if (_dbContext.Koinotarxes == null)
                    return null!;
                return await _dbContext.Koinotarxes.FirstOrDefaultAsync(ko => ko.Id == id);
            case Thesi.Tomearxis:
                if (_dbContext.Tomearxes == null)
                    return null!;
                return await _dbContext.Tomearxes.FirstOrDefaultAsync(to => to.Id == id);
            case Thesi.Ekpaideutis:
                throw new NotImplementedException();
            default:
                break;
        }
        return null!;
    }

    public async Task<bool> UpdateStelexosInDb(Stelexos stelexos)
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
            _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
            LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
            if (transaction != null)
                await transaction.RollbackAsync();

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
            _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
            LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);

            if (transaction != null)
                await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<Stelexos> GetStelexosByNameInDb(string name, Thesi? thesi)
    {
        if (string.IsNullOrEmpty(name) || thesi is null)
            return null!;

        if (thesi == Thesi.None)
        {
            IQueryable<Stelexos> query = _dbContext.Omadarxes!.Cast<Stelexos>()
                .Concat(_dbContext.Koinotarxes!.Cast<Stelexos>())
                .Concat(_dbContext.Tomearxes!.Cast<Stelexos>());
            return await query.FirstOrDefaultAsync(e => e.FullName == name);
        }

        return thesi switch
        {
            Thesi.Omadarxis => await _dbContext.Omadarxes!.FirstOrDefaultAsync(o => o.FullName == name),
            Thesi.Koinotarxis => await _dbContext.Koinotarxes!.FirstOrDefaultAsync(k => k.FullName == name),
            Thesi.Tomearxis => await _dbContext.Tomearxes!.FirstOrDefaultAsync(t => t.FullName == name),
            Thesi.Ekpaideutis => await _dbContext.Ekpaideutes!.FirstOrDefaultAsync(t => t.FullName == name),
            Thesi.None => throw new NotImplementedException(),
            null => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(thesi), thesi, null)
        };
    }

    private async Task<IEnumerable<Omadarxis>> GetOmadarxesAnaXwro(string xwrosName)
    {
        if (string.IsNullOrEmpty(xwrosName))
            return await GetAllOmadarxes();

        var isXwrosAnKoinotita = _dbContext.Koinotites!.Any(k => k.Name.Equals(xwrosName));
        if (isXwrosAnKoinotita)
            return await GetOmadarxesAnaKoinotita(xwrosName);
        else
        {
            var isXwrosATomeas = _dbContext.Tomeis!.Any(t => t.Name.Equals(xwrosName));
            if (isXwrosATomeas)
                return await GetOmadarxesAnaTomea(xwrosName);
            else
                return await GetAllOmadarxes();
        }
    }

    private async Task<IEnumerable<Koinotarxis>> GetKoinotarxesAnaXwro(string xwrosName)
    {
        if (string.IsNullOrEmpty(xwrosName))
            return await _dbContext.Koinotarxes!.ToListAsync();
        
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
        return await _dbContext.Skines!.Where(k => k.Koinotita.Name == name).Select(k => k.Omadarxis).ToListAsync();
    }

    private async Task<List<Omadarxis>> GetOmadarxesAnaTomea(string? name)
    {
        if (string.IsNullOrEmpty(name))
            return await _dbContext.Omadarxes!.ToListAsync();
        return await _dbContext.Skines!.Where(sk => sk.Koinotita.Tomeas.Name == name).Select(k => k.Omadarxis).ToListAsync();
    }

    private async Task<List<Omadarxis>> GetAllOmadarxes()
    {
        return await _dbContext.Omadarxes!.ToListAsync();
    }

    private async Task<List<Koinotarxis>> GetKoinotarxesAnaTomea(string? tomeasName)
    {
        if (string.IsNullOrEmpty(tomeasName))
            return await _dbContext.Koinotarxes!.ToListAsync();

        return await _dbContext.Koinotites!.Where(k => k.Tomeas.Name == tomeasName).Select(k => k.Koinotarxis).ToListAsync();
    }
}

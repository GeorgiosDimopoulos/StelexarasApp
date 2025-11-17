using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.DataAccess.Repositories;

public class StaffRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IStaffRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<StaffRepository> _logger = loggerFactory.CreateLogger<StaffRepository>();

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

    public async Task<IEnumerable<IStelexos>> GetStelexoiAnaXwroInDb(Thesi thesi, string? xwrosName, StelexosQueryParameters? queryParameters)
    {
        try
        {
            return thesi switch
            {
                Thesi.Omadarxis => await GetOmadarxesAnaXwro(xwrosName, queryParameters as OmadarxisQueryParameters ?? throw new ArgumentNullException(nameof(TomearxisQueryParameters))),
                Thesi.Koinotarxis => await GetKoinotarxesAnaXwro(xwrosName, queryParameters as KoinotarxisQueryParameters ?? throw new ArgumentNullException(nameof(TomearxisQueryParameters))),
                Thesi.Tomearxis => await GetTomearxes(queryParameters as TomearxisQueryParameters ?? throw new ArgumentNullException(nameof(TomearxisQueryParameters))),
                Thesi.Ekpaideutis => await _dbContext.Ekpaideutes!.ToListAsync(),
                Thesi.None => throw new NotImplementedException(),
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

    public async Task<bool> UpdateStelexosInDb(int id, IStelexos stelexos)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        var parts = stelexos.FullName.Trim().Split(' ');
        if (parts.Length < 2)
        {
            ArgumentException argumentException = new("Invalid FullName", nameof(stelexos.FullName));
            throw argumentException;
        }

        var existingStelexos = await GetStelexosByIdInDb(id);
        if (existingStelexos == null)
            return false;

        existingStelexos.XwrosName = stelexos.XwrosName;
        existingStelexos.FullName = stelexos.FullName;
        existingStelexos.Tel = stelexos.Tel;
        existingStelexos.Age = stelexos.Age;
        existingStelexos.Sex = stelexos.Sex;

        try
        {
            switch (stelexos.Thesi)
            {
                case Thesi.Omadarxis:
                    if (_dbContext.Omadarxes == null)
                        return false;
                    _dbContext.Omadarxes.Update((Omadarxis)existingStelexos);
                    break;
                case Thesi.Koinotarxis:
                    if (_dbContext.Koinotarxes == null)
                        return false;
                    _dbContext.Koinotarxes.Update((Koinotarxis)existingStelexos);
                    break;
                case Thesi.Tomearxis:
                    if (_dbContext.Tomearxes == null)
                        return false;
                    _dbContext.Tomearxes.Update((Tomearxis)existingStelexos);
                    break;
                case Thesi.Ekpaideutis:
                    if (_dbContext.Ekpaideutes == null)
                        return false;
                    _dbContext.Ekpaideutes.Update((Ekpaideutis)existingStelexos);
                    break;
                case Thesi.None:
                    break;
                default:
                    throw new ArgumentException("Invalid Thesi value!", nameof(existingStelexos.Thesi));
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

    public async Task<IStelexos> GetStelexosByNameInDb(string name, Thesi? thesi, StelexosQueryParameters stelexosQueryParameters)
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

            // ToDo: implement it
            if (stelexosQueryParameters.IncludeXwros)
            {
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

    private async Task<IEnumerable<Omadarxis>> GetOmadarxesAnaXwro(string? xwrosName, OmadarxisQueryParameters omadarxisQueryParameters)
    {
        var omadarxes = _dbContext.Omadarxes.AsQueryable();

        if (omadarxisQueryParameters.IncludePaidia && string.IsNullOrWhiteSpace(xwrosName))
            return await omadarxes.Include(om => om.Skini.Paidia).ToListAsync();

        if (string.IsNullOrWhiteSpace(xwrosName))
            return await omadarxes.ToListAsync();

        if (omadarxisQueryParameters.IncludePaidia)
        {
            omadarxes = omadarxes.Include(om => om.Skini.Paidia);
        }
        if (omadarxisQueryParameters.IncludeXwros)
        {
            omadarxes = omadarxes.Include(om => om.Skini);
        }

        var isXwrosAnKoinotita = await _dbContext.Koinotites!.AnyAsync(k => k.Name.Equals(xwrosName));
        if (isXwrosAnKoinotita)
        {
            return await omadarxes.Where(om => om.Skini.Koinotita.Name.Equals(xwrosName))
                                  .ToListAsync();
        }
        else
        {
            var isXwrosATomeas = await _dbContext.Tomeis!.AnyAsync(t => t.Name.Equals(xwrosName));
            if (isXwrosATomeas)
            {
                return await omadarxes.Where(sk => sk.Skini.Koinotita.Tomeas.Name.Equals(xwrosName))
                                      .ToListAsync();
            }
        }

        return await _dbContext.Omadarxes.ToListAsync();
    }

    private async Task<IEnumerable<Ekpaideutis>> GetTomearxes(TomearxisQueryParameters tomearxisQueryParameters)
    {
        if (_dbContext.Ekpaideutes == null || !_dbContext.Ekpaideutes.Any())
            return null!;

        if (tomearxisQueryParameters.IncludeXwros)
        {
            _dbContext.Ekpaideutes.Include(t => t.XwrosName);
        }

        return await _dbContext.Ekpaideutes!.ToListAsync();
    }

    private async Task<IEnumerable<Koinotarxis>> GetKoinotarxesAnaXwro(string? xwrosName, KoinotarxisQueryParameters koinotarxisQueryParameters)
    {
        if (_dbContext.Koinotarxes == null || !_dbContext.Koinotarxes!.Any())
            return null!;

        if (koinotarxisQueryParameters.IncludeXwros)
        {
            _dbContext.Koinotarxes.Include(k => k.XwrosName);
        }
        if (koinotarxisQueryParameters.IncludeTomeas)
        {
            _dbContext.Koinotarxes.Include(k => k.Koinotita.Tomeas);
        }

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

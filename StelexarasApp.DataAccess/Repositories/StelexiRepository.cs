﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories
{
    public class StelexiRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IStelexiRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ILogger<StelexiRepository> _logger = loggerFactory.CreateLogger<StelexiRepository>();

        public async Task<bool> AddStelexosInDb(Stelexos stelexi)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (stelexi == null)
                return false;            

            try
            {
                switch (stelexi.Thesi)
                {
                    case Thesi.Omadarxis:
                        _dbContext.Omadarxes?.Add((Omadarxis)stelexi);
                        break;
                    case Thesi.Koinotarxis:
                        _dbContext.Koinotarxes?.Add((Koinotarxis)stelexi);
                        break;
                    case Thesi.Tomearxis:
                        _dbContext.Tomearxes?.Add((Tomearxis)stelexi);
                        break;
                    case Thesi.Ekpaideutis:
                        throw new NullReferenceException("Not yet implemented!");
                    case Thesi.None:
                        break;
                    default:
                        throw new ArgumentException("Invalid Thesi value!", nameof(stelexi.Thesi));
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

        public async Task<IEnumerable<Stelexos>> GetStelexoiAnaThesiFromDb(Thesi thesi)
        {
            try
            {
                return thesi switch
                {
                    Thesi.Omadarxis => await _dbContext.Omadarxes!.ToListAsync(),
                    Thesi.Koinotarxis => await _dbContext.Koinotarxes!.ToListAsync(),
                    Thesi.Tomearxis => await _dbContext.Tomearxes!.ToListAsync(),
                    Thesi.None => throw new NotImplementedException(),
                    Thesi.Ekpaideutis => throw new NotImplementedException(),
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

        public async Task<Stelexos> FindStelexosByIdInDb(int id, Thesi thesi)
        {
            if (thesi == Thesi.None || id <= 0 || _dbContext is null)
                return null!;

            switch (thesi)
            {
                case Thesi.None:
                    break;
                case Thesi.Omadarxis:
                    if (_dbContext.Omadarxes == null)
                    {
                        return null!;
                    }
                    return await _dbContext.Omadarxes!.FirstAsync(om => om.Id == id);
                case Thesi.Koinotarxis:
                    if (_dbContext.Koinotarxes == null)
                    {
                        return null!;
                    }
                    return await _dbContext.Koinotarxes.FirstAsync(ko => ko.Id == id);
                case Thesi.Tomearxis:
                    if (_dbContext.Tomearxes == null)
                    {
                        return null!;
                    }
                    return await _dbContext.Tomearxes.FirstAsync(to => to.Id == id);
                case Thesi.Ekpaideutis:
                    throw new NotImplementedException();
                default:
                    break;
            }

            return null!;
        }

        public async Task<bool> UpdateStelexosInDb(Stelexos stelexi)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                switch (stelexi.Thesi)
                {
                    case Thesi.Omadarxis:
                        _dbContext.Omadarxes?.Update((Omadarxis)stelexi);
                        break;
                    case Thesi.Koinotarxis:
                        _dbContext.Koinotarxes?.Update((Koinotarxis)stelexi);
                        break;
                    case Thesi.Tomearxis:
                        _dbContext.Tomearxes?.Update((Tomearxis)stelexi);
                        break;
                    case Thesi.Ekpaideutis:
                        throw new NotImplementedException();
                    case Thesi.None:
                        break;
                    default:
                        throw new ArgumentException("Invalid Thesi value!", nameof(stelexi.Thesi));
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
                {
                    await transaction.RollbackAsync();
                }

                return false;
            }
        }

        public async Task<bool> MoveOmadarxisToAnotherSkiniInDb(int id, int newSkiniId)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (id <= 0 || newSkiniId <= 0 || _dbContext.Skines is null || _dbContext.Omadarxes is null)
            {
                return false;
            }

            try
            {
                var omadarxisInDb = await _dbContext.Omadarxes.Include(o => o.Skini).FirstOrDefaultAsync(o => o.Id == id);
                var newSkini = await _dbContext.Skines.FirstOrDefaultAsync(s => s.Id == newSkiniId);

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
    }
}
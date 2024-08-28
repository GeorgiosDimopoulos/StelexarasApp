using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class StelexiService : IStelexiService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public StelexiService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AddStelexosInDbAsync(StelexosDto stelexosDto, Thesi thesi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            Stelexos? stelexos = MapDtoToEntity(stelexosDto, thesi);

            if (stelexos == null)
            {
                await transaction.RollbackAsync();
                return false;
            }

            try
            {
                switch (thesi)
                {
                    case Thesi.Omadarxis:
                        _dbContext.Omadarxes.Add((Omadarxis)stelexos);
                        break;
                    case Thesi.Koinotarxis:
                        _dbContext.Koinotarxes.Add((Koinotarxis)stelexos);
                        break;
                    case Thesi.Tomearxis:
                        _dbContext.Tomearxes.Add((Tomearxis)stelexos);
                        break;
                    default:
                        throw new ArgumentException("Invalid Thesi value", nameof(thesi));
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteStelexosInDb(int id, Thesi thesi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                switch (thesi)
                {
                    case Thesi.Omadarxis:
                        var omadarxis = await _dbContext.Omadarxes.FindAsync(id);
                        if(omadarxis != null)
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
                await transaction.CommitAsync();
                return changes > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Stelexos>> GetStelexoi(Thesi thesi)
        {
            try
            {
                return thesi switch
                {
                    Thesi.Omadarxis => await _dbContext.Omadarxes.ToListAsync(),
                    Thesi.Koinotarxis => await _dbContext.Koinotarxes.ToListAsync(),
                    Thesi.Tomearxis => await _dbContext.Tomearxes.ToListAsync(),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Stelexos> GetStelexosById(int id, Thesi thesi)
        {
            try
            {
                if (thesi == Thesi.None|| id <= 0 || _dbContext is null)
                    return null;

                return thesi switch
                {
                    Thesi.Omadarxis => await _dbContext.Omadarxes.FindAsync(id),
                    Thesi.Koinotarxis => await _dbContext.Koinotarxes.FindAsync(id),
                    Thesi.Tomearxis => await _dbContext.Tomearxes.FindAsync(id),
                    Thesi.None => throw new NotImplementedException(),
                    Thesi.Ekpaideutis => throw new NotImplementedException(),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> MoveStelexosToNewSkini(int Id, int newSkiniId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // ToDo: implementation is missing!
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> UpdateStelexosInDb(int id, StelexosDto stelexosDto, Thesi thesi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // ToDo: implementation is missing!
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private Stelexos MapDtoToEntity(StelexosDto dto, Thesi thesi)
        {
            return thesi switch
            {
                Thesi.Omadarxis => _mapper.Map<Omadarxis>(dto),
                Thesi.Koinotarxis => _mapper.Map<Koinotarxis>(dto),
                Thesi.Tomearxis => _mapper.Map<Tomearxis>(dto),
                _ => _mapper.Map<Stelexos>(dto),
            };
        }
    }
}

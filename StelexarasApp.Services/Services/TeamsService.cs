using AutoMapper;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.Library.Models.Logs;
using StelexarasApp.Library.QueryParameters;

namespace StelexarasApp.Services.Services
{
    public class TeamsService(IMapper mapper, ITeamsRepository teamsRepository) : ITeamsService
    {
        private readonly ITeamsRepository _teamsRepository = teamsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> AddSkiniInService(SkiniDto skiniDto)
        {
            try
            {
                if (skiniDto is null || string.IsNullOrEmpty(skiniDto.Name) || string.IsNullOrEmpty(skiniDto.KoinotitaName))
                    return false;

                var skini = _mapper.Map<Skini>(skiniDto);
                return await _teamsRepository.AddSkiniInDb(skini);
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                return false;
            }
        }

        public async Task<bool> AddKoinotitaInService(KoinotitaDto koinotitaDto)
        {
            try
            {
                if (koinotitaDto is null || string.IsNullOrEmpty(koinotitaDto.Name))
                    return false;

                var koinotita = _mapper.Map<Koinotita>(koinotitaDto);

                koinotita.Tomeas = await _teamsRepository.GetTomeaByNameInDb(new(), koinotitaDto.TomeasName);
                return await _teamsRepository.AddKoinotitaInDb(koinotita);
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                return false;
            }
        }

        public async Task<IEnumerable<SkiniDto>> GetAllSkinesInService(SkiniQueryParameters? skiniQueryParameters)
        {
            try
            {
                var skini = await _teamsRepository.GetSkinesInDb(skiniQueryParameters);
                return _mapper.Map<IEnumerable<SkiniDto>>(skini);
            }
            catch
            {
                return new List<SkiniDto>();
            }
        }

        public async Task<SkiniDto> GetSkiniByNameInService(SkiniQueryParameters skiniQueryParameters, string name)
        {
            var skini = await _teamsRepository.GetSkiniByNameInDb(skiniQueryParameters, name);
            return _mapper.Map<SkiniDto>(skini);
        }

        public async Task<IEnumerable<SkiniDto>> GetSkinesAnaKoinotitaInService(SkiniQueryParameters? skiniQueryParameters, string koinotitaName)
        {
            var skines = await _teamsRepository.GetSkinesAnaKoinotitaInDb(skiniQueryParameters, koinotitaName);
            return _mapper.Map<IEnumerable<SkiniDto>>(skines);
        }

        public async Task<IEnumerable<SkiniDto>> GetSkinesEkpaideuomenonInService(SkiniQueryParameters skiniQueryParameters)
        {
            var skines = await _teamsRepository.GetSkinesEkpaideuomenonInDb(skiniQueryParameters);
            return _mapper.Map<IEnumerable<SkiniDto>>(skines);
        }

        public async Task<IEnumerable<KoinotitaDto>> GetAllKoinotitesInService(KoinotitaQueryParameters koinotitaQueryParameters)
        {
            var koinotitaInDb = await _teamsRepository.GetKoinotitesInDb(koinotitaQueryParameters);
            return _mapper.Map<IEnumerable<KoinotitaDto>>(koinotitaInDb);
        }

        public async Task<IEnumerable<KoinotitaDto>> GetKoinotitesAnaTomeaInService(KoinotitaQueryParameters koinotitaQueryParameters, int tomeaId)
        {
            var koinotitaInDb = await _teamsRepository.GetKoinotitesAnaTomeaInDb(koinotitaQueryParameters, tomeaId);
            return _mapper.Map<IEnumerable<KoinotitaDto>>(koinotitaInDb);
        }

        public async Task<IEnumerable<TomeasDto>> GetAllTomeisInService(TomeasQueryParameters tomeasQueryParameters)
        {
            var tomeisInDb = await _teamsRepository.GetTomeisInDb(tomeasQueryParameters);
            return _mapper.Map<IEnumerable<TomeasDto>>(tomeisInDb);
        }

        public async Task<KoinotitaDto> GetKoinotitaByNameInService(KoinotitaQueryParameters koinotitaQueryParameters, string name)
        {
            var skini = await _teamsRepository.GetKoinotitaByNameInDb(koinotitaQueryParameters, name);
            return _mapper.Map<KoinotitaDto>(skini);
        }

        public Task<bool> UpdateKoinotitaInService(KoinotitaDto koinotitaDto)
        {
            var koinotita = _mapper.Map<Koinotita>(koinotitaDto);
            return _teamsRepository.UpdateKoinotitaInDb(koinotita);
        }

        public Task<bool> UpdateSkiniInService(SkiniDto skiniDto)
        {
            var skini = _mapper.Map<Skini>(skiniDto);
            return _teamsRepository.UpdateSkiniInDb(skini);
        }

        public Task<bool> UpdateTomeasInService(TomeasDto tomeasDto)
        {
            var tomeas = _mapper.Map<Tomeas>(tomeasDto);
            return _teamsRepository.UpdateTomeasInDb(tomeas);
        }

        public Task<bool> DeleteSkiniInService(int skiniId)
        {
            return _teamsRepository.DeleteSkiniInDb(skiniId);
        }

        public Task<bool> DeleteKoinotitaInService(int koinotitaId)
        {
            return _teamsRepository.DeleteKoinotitaInDb(koinotitaId);
        }

        public Task<bool> DeleteTomeasInService(string n)
        {
            return _teamsRepository.DeleteTomeasInDb(n);
        }

        public async Task<TomeasDto> GetTomeaByNameInService(TomeasQueryParameters tomeasQueryParameters, string name)
        {
            var tomeasInDb = await _teamsRepository.GetTomeaByNameInDb(tomeasQueryParameters, name);
            return _mapper.Map<TomeasDto>(tomeasInDb);
        }

        public async Task<bool> UpdateTomeaInService(TomeasDto tomeas)
        {
            try
            {
                var tomea = _mapper.Map<Tomeas>(tomeas);
                var array = new Koinotita [tomeas.KoinotitesNumber];
                tomea.Koinotites = [.. array];
                return await _teamsRepository.UpdateTomeasInDb(tomea);
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                return false;
            }
        }

        public async Task<bool> AddTomeasInService(TomeasDto tomeasDto)
        {
            try
            {
                if (tomeasDto is null || string.IsNullOrEmpty(tomeasDto.Name))
                    return false;
                var tomeas = _mapper.Map<Tomeas>(tomeasDto);
                return await _teamsRepository.AddTomeasInDb(tomeas);
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                return false;
            }
        }

        public Task<bool> HasData()
        {
            if (!_teamsRepository.GetSkinesInDb(new()).Result.Any() &&
                !_teamsRepository.GetKoinotitesAnaTomeaInDb(new(), 2).Result.Any() &&
                !_teamsRepository.GetKoinotitesAnaTomeaInDb(new(), 1).Result.Any())
                return Task.FromResult(false);
            return Task.FromResult(true);
        }

        public async Task<bool> CheckStelexousXwroNameInService(IStelexosDto stelexosDto, string xwrosName)
        {
            switch (stelexosDto.Thesi)
            {
                case Thesi.Omadarxis:
                    var skini = await _teamsRepository.GetSkiniByNameInDb(new(), xwrosName);
                    if (skini == null)
                    {
                        Console.WriteLine($"Skini {xwrosName} doesnt exist in DB!");
                        return false;
                    }
                    if (stelexosDto.Id == skini.OmadarxisId)
                    {
                        Console.WriteLine($"Skini {xwrosName} already has another Omadarxis!");
                        return false;
                    }
                    return true;
                case Thesi.Koinotarxis:
                    var koinotita = await _teamsRepository.GetKoinotitaByNameInDb(new(), xwrosName);
                    if (koinotita == null)
                    {
                        Console.WriteLine($"Koinotita {xwrosName} doesnt exist in DB!");
                        return false;
                    }
                    if (stelexosDto.Id == koinotita.KoinotarxisId)
                    {
                        Console.WriteLine($"Koinotita {xwrosName} already has another Koinotarxi!");
                        return false;
                    }
                    return true;
                case Thesi.Tomearxis:
                    var tomeas = await _teamsRepository.GetTomeaByNameInDb(new(), xwrosName);
                    if (tomeas == null)
                    {
                        Console.WriteLine($"Tomeas {xwrosName} doesnt exist in DB!");
                        return false;
                    }
                    if (stelexosDto.Id == tomeas.TomearxisId)
                    {
                        Console.WriteLine($"Tomeas {xwrosName} already has another Tomearxi!");
                        return false;
                    }
                    return true;
                case Thesi.Ekpaideutis:
                    throw new NotImplementedException();
                default:
                    return false;
            }
        }
    }
}

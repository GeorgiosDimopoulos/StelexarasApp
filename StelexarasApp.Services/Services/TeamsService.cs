using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly IMapper _mapper;

        public TeamsService(IMapper mapper, ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddSkiniInService(SkiniDto skiniDto)
        {
            var skini = _mapper.Map<Skini>(skiniDto);
            return await _teamsRepository.AddSkiniInDb(skini);
        }

        public async Task<bool> AddKoinotitaInService(KoinotitaDto koinotitaDto)
        {
            var koinotita = _mapper.Map<Koinotita>(koinotitaDto);
            return await _teamsRepository.AddKoinotitaInDb(koinotita);
        }

        public async Task<IEnumerable<SkiniDto>> GetAllSkinesInService()
        {
            var skini = await _teamsRepository.GetSkinesInDb();
            return _mapper.Map<IEnumerable<SkiniDto>>(skini);
        }

        public async Task<SkiniDto> GetSkiniByNameInService
            (string name)
        {
            var skini = await _teamsRepository.GetSkiniByNameInDb(name);
            return _mapper.Map<SkiniDto>(skini);
        }

        public async Task<IEnumerable<SkiniDto>> GetSkinesAnaKoinotitaInService(string koinotitaName)
        {
            var skines = await _teamsRepository.GetSkinesAnaKoinotitaInDb(koinotitaName);
            return _mapper.Map<IEnumerable<SkiniDto>>(skines);
        }

        public async Task<IEnumerable<SkiniDto>> GetSkinesEkpaideuomenonInService()
        {
            var skines = await _teamsRepository.GetSkinesEkpaideuomenonInDb();
            return _mapper.Map<IEnumerable<SkiniDto>>(skines);
        }

        public async Task<IEnumerable<KoinotitaDto>> GetAllKoinotitesInService()
        {
            var koinotitaInDb = await _teamsRepository.GetKoinotitesInDb();
            return _mapper.Map<IEnumerable<KoinotitaDto>>(koinotitaInDb);
        }

        public async Task<IEnumerable<KoinotitaDto>> GetKoinotitesAnaTomeaInService(int tomeaId)
        {
            var koinotitaInDb = await _teamsRepository.GetKoinotitesAnaTomeaInDb(tomeaId);
            return _mapper.Map<IEnumerable<KoinotitaDto>>(koinotitaInDb);
        }

        public async Task<IEnumerable<TomeasDto>> GetAllTomeisInService()
        {
            var tomeisInDb = await _teamsRepository.GetTomeisInDb();
            return _mapper.Map<IEnumerable<TomeasDto>>(tomeisInDb);
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

        public Task<bool> DeleteTomeasInService(int tomeasId)
        {
            return _teamsRepository.DeleteTomeasInDb(tomeasId);
        }

        public async Task<TomeasDto> GetTomeaByNameInService(string name)
        {
            var tomeasInDb = await _teamsRepository.GetTomeaByNameInDb(name);
            return _mapper.Map<TomeasDto>(tomeasInDb);
        }

        public async Task<bool> UpdateTomeaInService(TomeasDto tomeas)
        {
            var tomea = _mapper.Map<Tomeas>(tomeas);
            return await _teamsRepository.UpdateTomeasInDb(tomea);
        }

        public async Task<SkiniDto> GetKoinotitaByNameInService(string name)
        {
            var skini = await _teamsRepository.GetSkiniByNameInDb(name);
            return _mapper.Map<SkiniDto>(skini);
        }

        public Task<bool> AddTomeasInService(TomeasDto tomeasDto)
        {
            var tomeas = _mapper.Map<Tomeas>(tomeasDto);
            return _teamsRepository.AddTomeasInDb(tomeas);
        }

        public Task<bool> HasData()
        {
            if (!_teamsRepository.GetSkinesInDb().Result.Any() &&
                !_teamsRepository.GetKoinotitesAnaTomeaInDb(2).Result.Any() &&
                !_teamsRepository.GetKoinotitesAnaTomeaInDb(1).Result.Any())
                return Task.FromResult(false);
            return Task.FromResult(true);
        }

        public async Task<bool> CheckStelexousXwroNameInService(StelexosDto stelexosDto, string xwrosName)
        {
            switch (stelexosDto.Thesi)
            {
                case Thesi.Omadarxis:
                    var skini = await _teamsRepository.GetSkiniByNameInDb(xwrosName);
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
                    var koinotita = await _teamsRepository.GetKoinotitaByNameInDb(xwrosName);
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
                    var tomeas = await _teamsRepository.GetTomeaByNameInDb(xwrosName);
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

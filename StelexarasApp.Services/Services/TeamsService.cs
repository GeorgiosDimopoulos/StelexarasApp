using AutoMapper;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
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

        public Task<IEnumerable<Skini>> GetSkines()
        {
            return _teamsRepository.GetSkinesInDb();
        }

        public Task<Skini> GetSkiniByName(string name)
        {
            return _teamsRepository.GetSkiniByNameInDb(name);
        }

        public Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaInService(string koinotitaName)
        {
            return _teamsRepository.GetSkinesAnaKoinotitaInDb(koinotitaName);
        }

        public Task<IEnumerable<Skini>> GetSkinesEkpaideuomenonInService()
        {
            return _teamsRepository.GetSkinesEkpaideuomenonInDb();
        }

        public Task<IEnumerable<Koinotita>> GetKoinotitesInService()
        {
            return _teamsRepository.GetKoinotitesInDb();
        }

        public Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomeaInService(int tomeaId)
        {
            return _teamsRepository.GetKoinotitesAnaTomeaInDb(tomeaId);
        }

        public Task<IEnumerable<Tomeas>> GetTomeisInService()
        {
            return _teamsRepository.GetTomeisInDb();
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
    }
}

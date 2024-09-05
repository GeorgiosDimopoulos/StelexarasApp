using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;

namespace StelexarasApp.Services.Services
{
    public class TeamsService
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly IMapper _mapper;

        public TeamsService(IMapper mapper, ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
        }

        public Task<IEnumerable<Skini>> GetSkines()
        {
            return _teamsRepository.GetSkinesInDb();
        }

        public Task<Skini> GetSkiniByName(string name)
        {
            return _teamsRepository.GetSkiniByNameInDb(name);
        }

        public Task<IEnumerable<Skini>> GetSkinesAnaKoinotita(string koinotitaName)
        {
            return _teamsRepository.GetSkinesAnaKoinotitaInDb(koinotitaName);
        }

        public Task<IEnumerable<Skini>> GetSkinesEkpaideuomenon()
        {
            return _teamsRepository.GetSkinesEkpaideuomenonInDb();
        }

        public Task<IEnumerable<Koinotita>> GetKoinotites()
        {
            return _teamsRepository.GetKoinotitesInDb();
        }

        public Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomea(int tomeaId)
        {
            return _teamsRepository.GetKoinotitesAnaTomeaInDb(tomeaId);
        }

        public Task<IEnumerable<Tomeas>> GetTomeis()
        {
            return _teamsRepository.GetTomeisInDb();
        }

        public Task<bool> UpdateKoinotita(KoinotitaDto koinotitaDto)
        {
            var koinotita = _mapper.Map<Koinotita>(koinotitaDto);
            return _teamsRepository.UpdateKoinotitaInDb(koinotita);
        }

        public Task<bool> UpdateSkini(SkiniDto skiniDto)
        {
            var skini = _mapper.Map<Skini>(skiniDto);
            return _teamsRepository.UpdateSkiniInDb(skini);
        }

        public Task<bool> UpdateTomeas(TomeasDto tomeasDto)
        {
            var tomeas = _mapper.Map<Tomeas>(tomeasDto);
            return _teamsRepository.UpdateTomeasInDb(tomeas);
        }

        public Task<bool> DeleteSkini(int skiniId)
        {
            return _teamsRepository.DeleteSkiniInDb(skiniId);
        }

        public Task<bool> DeleteKoinotita(int koinotitaId)
        {
            return _teamsRepository.DeleteKoinotitaInDb(koinotitaId);
        }

        public Task<bool> DeleteTomeas(int tomeasId)
        {
            return _teamsRepository.DeleteTomeasInDb(tomeasId);
        }
    }
}

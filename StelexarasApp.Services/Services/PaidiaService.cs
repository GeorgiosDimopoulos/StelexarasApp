using AutoMapper;
using StelexarasApp.DataAccess.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class PaidiaService : IPaidiaService
    {
        // private readonly ILogger<TeamsService> _logger;
        private readonly IPaidiRepository _paidiRepository;
        private readonly IMapper _mapper;

        public PaidiaService(IPaidiRepository paidiRepository, IMapper mapper)
        {
            try
            {
                _paidiRepository = paidiRepository;
                _mapper = mapper;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> AddPaidiInDbAsync(PaidiDto paidiDto)
        {
            var paidi = _mapper.Map<Paidi>(paidiDto);
            var result = await _paidiRepository.RemovePaidiAsync(paidi);

            if (!result)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeletePaidiInDb(int id)
        {
            var result = await _paidiRepository.GetPaidiById(id);
            if (result == null)
            {
                return false;
            }
            
            return true;
        }

        public Task<IEnumerable<Paidi>> GetPaidia(PaidiType type)
        {
            return _paidiRepository.GetPaidia(type);
        }

        public Task<Paidi> GetPaidiById(int id)
        {
            return _paidiRepository.GetPaidiById(id);
        }

        public Task<IEnumerable<Skini>> GetSkines()
        {
            return _paidiRepository.GetSkines();
        }

        public Task<Skini> GetSkiniByName(string name)
        {
            return _paidiRepository.GetSkiniByName(name);
        }

        public async Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId)
        {
            var result = await _paidiRepository.MovePaidiToNewSkini(paidiId, newSkiniId);

            if (!result)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdatePaidiInDb(PaidiDto paidiDto)
        {
            var paidi = _mapper.Map<Paidi>(paidiDto);
            var result = await _paidiRepository.UpdatePaidiInDb(paidi);

            if (!result)
            {
                return false;
            }

            return true;
        }
    }
}
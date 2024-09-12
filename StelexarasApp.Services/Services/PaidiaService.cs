using AutoMapper;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class PaidiaService : IPaidiaService
    {
        // private readonly ILogger<TeamsService> _logger;
        private readonly IPaidiRepository? _paidiRepository;
        private readonly IMapper? _mapper;

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
            if (paidi == null)
            {
                return false;
            }

            return await _paidiRepository.AddPaidiInDb(paidi);
        }

        public async Task<bool> DeletePaidiInDb(int id)
        {
            var paidi = await _paidiRepository.GetPaidiByIdFromDb(id);
            if (paidi == null)
                return false;

            return await _paidiRepository.DeletePaidiInDb(paidi);
        }

        public Task<IEnumerable<Paidi>> GetPaidia(PaidiType type)
        {
            return _paidiRepository.GetPaidiaFromDb(type);
        }

        public Task<Paidi> GetPaidiById(int id)
        {
            return _paidiRepository.GetPaidiByIdFromDb(id);
        }

        public async Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId)
        {
            var result = await _paidiRepository.MovePaidiToNewSkiniInDb(paidiId, newSkiniId);

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
using AutoMapper;
using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Library.Dtos.Atoma;

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

        public async Task<bool> AddPaidiInService(PaidiDto paidiDto)
        {
            if (paidiDto == null || _mapper == null || _paidiRepository is null)
                return false;

            var paidi = _mapper.Map<Paidi>(paidiDto);
            if (paidi == null)
                return false;

            return await _paidiRepository.AddPaidiInDb(paidi);
        }

        public async Task<bool> DeletePaidiInService(int id)
        {
            if (id <= 0 || _mapper == null || _paidiRepository is null)
                return false;

            var paidi = await _paidiRepository.GetPaidiByIdFromDb(id);
            if (paidi == null)
                return false;

            return await _paidiRepository.DeletePaidiInDb(paidi);
        }

        public Task<IEnumerable<Paidi>> GetPaidiaInService(PaidiType type)
        {
            if (_mapper == null || _paidiRepository is null)
                return null!;

            return _paidiRepository.GetPaidiaFromDb(type);
        }

        public Task<Paidi> GetPaidiByIdInService(int id)
        {
            if (id <= 0 || _mapper == null || _paidiRepository is null)
                return null!;

            return _paidiRepository.GetPaidiByIdFromDb(id);
        }

        public async Task<bool> MovePaidiToNewSkiniInService(int paidiId, int newSkiniId)
        {
            if (paidiId <= 0 || newSkiniId <= 0 || _mapper == null || _paidiRepository is null)
                return false;

            var result = await _paidiRepository.MovePaidiToNewSkiniInDb(paidiId, newSkiniId);
            if (!result)
                return false;

            return true;
        }

        public async Task<bool> UpdatePaidiInService(PaidiDto paidiDto)
        {
            if (paidiDto == null || _mapper == null || _paidiRepository is null)
                return false;

            var paidi = _mapper.Map<Paidi>(paidiDto);
            var result = await _paidiRepository.UpdatePaidiInDb(paidi);

            if (!result)
                return false;
            return true;
        }
    }
}
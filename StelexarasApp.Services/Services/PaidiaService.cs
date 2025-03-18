using AutoMapper;
using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Library.Dtos.Atoma;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace StelexarasApp.Services.Services
{
    public class PaidiaService : IPaidiaService
    {
        private readonly ILogger<PaidiaService> _logger;
        private readonly IPaidiRepository? _paidiRepository;
        private readonly IMapper? _mapper;
        private readonly IValidator<PaidiDto> _paidiValidator;

        public PaidiaService(
            IPaidiRepository paidiRepository,
            IMapper mapper, 
            ILogger<PaidiaService> logger,
            IValidator<PaidiDto> paidiValidator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _paidiValidator = paidiValidator ?? throw new ArgumentNullException(nameof(paidiValidator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _paidiRepository = paidiRepository ?? throw new ArgumentNullException(nameof(paidiRepository));
        }

        public async Task<bool> AddPaidiInService(PaidiDto paidiDto)
        {
            if (_paidiValidator == null || _mapper == null || _paidiRepository == null)
                return false;

            var validationResult = _paidiValidator.Validate(paidiDto);

            if (!validationResult.IsValid)
            {
                return false;
            }
            else
            {
                if (paidiDto == null || _mapper == null || _paidiRepository is null)
                    return false;

                var paidi = _mapper.Map<Paidi>(paidiDto);
                if (paidi == null)
                    return false;

                return await _paidiRepository.AddPaidiInDb(paidi);
            }
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
            var validationResult = _paidiValidator.Validate(paidiDto);

            if (!validationResult.IsValid)
                return false;

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
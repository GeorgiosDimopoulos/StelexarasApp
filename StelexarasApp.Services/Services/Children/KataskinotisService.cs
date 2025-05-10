using AutoMapper;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace StelexarasApp.Services.Services.Children;

public class KataskinotisService : IKataskinotisService
{
    private readonly ILogger<EkpaideuomenosService> _logger;
    private readonly IPaidiRepository? _paidiRepository;
    private readonly IMapper? _mapper;
    private readonly IValidator<PaidiDtoBase> _paidiValidator;

    public KataskinotisService(
        IPaidiRepository paidiRepository,
        IMapper mapper,
        ILogger<EkpaideuomenosService> logger,
        IValidator<PaidiDtoBase> paidiValidator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _paidiValidator = paidiValidator ?? throw new ArgumentNullException(nameof(paidiValidator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _paidiRepository = paidiRepository ?? throw new ArgumentNullException(nameof(paidiRepository));
    }

    public async Task<bool> CreateKataskinotisInService(CreateKataskinotisRequest paidiDto)
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

    public async Task<bool> DeleteKataskinotisInService(DeleteKataskinotisRequest request)
    {
        if (request.Id <= 0 || _mapper == null || _paidiRepository is null)
            return false;

        var paidi = await _paidiRepository.GetPaidiByIdFromDb(request.Id);
        if (paidi == null)
            return false;

        return await _paidiRepository.DeletePaidiInDb(paidi);
    }

    public async Task<IEnumerable<KataskinotisResponse>> GetKataskinotesInService()
    {
        if (_mapper == null || _paidiRepository is null)
            return null!;

        var paidia = await _paidiRepository.GetPaidiaFromDb(PaidiType.Kataskinotis);
        if (paidia == null)
            return null!;

        var kataskinotes = paidia.OfType<Kataskinotis>().ToList();
        var kataskinotesResponse = _mapper.Map<IEnumerable<KataskinotisResponse>>(kataskinotes);
        if (kataskinotesResponse == null)
            return null!;
        return kataskinotesResponse;
    }

    public async Task<KataskinotisResponse> GetKataskinotisByIdInService(int id)
    {
        if (_mapper == null || _paidiRepository is null)
            return null!;

        Paidi paidi = await _paidiRepository.GetPaidiByIdFromDb(id);
        if (paidi == null)
            return null!;
        if (paidi is not Kataskinotis kataskinotis)
            return null!;

        return _mapper.Map<KataskinotisResponse>(kataskinotis);
    }

    public async Task<bool> MoveKataskinotisToNewSkiniInService(int paidiId, int newSkiniId)
    {
        if (paidiId <= 0 || newSkiniId <= 0 || _mapper == null || _paidiRepository is null)
            return false;

        var result = await _paidiRepository.MovePaidiToNewSkiniInDb(paidiId, newSkiniId);
        if (!result)
            return false;

        return true;
    }

    public async Task<bool> UpdateKataskinotisInService(UpdateKataskinotisRequest paidiDto)
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
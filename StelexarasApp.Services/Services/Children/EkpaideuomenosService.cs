using AutoMapper;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace StelexarasApp.Services.Services.Children;

public class EkpaideuomenosService : IEkpaideuomenosService
{
    private readonly ILogger<EkpaideuomenosService> _logger;
    private readonly IPaidiRepository? _paidiRepository;
    private readonly IMapper? _mapper;
    private readonly IValidator<PaidiDtoBase> _paidiValidator;

    public EkpaideuomenosService(
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

    public async Task<bool> CreateEkpaideuomenosInService(CreateEkpaideuomenosRequest paidiDto)
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

    public async Task<bool> DeleteEkpaideuomenosInService(DeleteEkpaideuomenosRequest request)
    {
        if (request.Id <= 0 || _mapper == null || _paidiRepository is null)
            return false;

        var paidi = await _paidiRepository.GetPaidiByIdFromDb(request.Id);
        if (paidi == null)
            return false;

        return await _paidiRepository.DeletePaidiInDb(paidi);
    }

    public async Task<IEnumerable<EkpaideuomenosResponse>> GetEkpaideuomenoiInService()
    {
        if (_mapper == null || _paidiRepository is null)
            return null!;

        var paidia = await _paidiRepository.GetPaidiaFromDb(PaidiType.Ekpaideuomenos);
        if (paidia == null)
            return null!;
        var ekpaideuomenoi = paidia.OfType<Ekpaideuomenos>().ToList();
        return _mapper.Map<IEnumerable<EkpaideuomenosResponse>>(ekpaideuomenoi);
    }

    public async Task<EkpaideuomenosResponse> GetEkpaideuomenosByIdInService(int id)
    {
        if (id <= 0 || _mapper == null || _paidiRepository is null)
            return null!;

        var paidi = await _paidiRepository.GetPaidiByIdFromDb(id);
        if (paidi == null)
            return null!;
        var ekpaideuomenos = _mapper.Map<EkpaideuomenosResponse>(paidi);
        return ekpaideuomenos;
    }

    public async Task<bool> MoveEkpaideuomenosToNewSkiniInService(int paidiId, int newSkiniId)
    {
        if (paidiId <= 0 || newSkiniId <= 0 || _mapper == null || _paidiRepository is null)
            return false;

        var result = await _paidiRepository.MovePaidiToNewSkiniInDb(paidiId, newSkiniId);
        if (!result)
            return false;

        return true;
    }

    public async Task<bool> UpdateEkpaideuomenosInService(UpdateEkpaideuomenosRequest paidiDto)
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
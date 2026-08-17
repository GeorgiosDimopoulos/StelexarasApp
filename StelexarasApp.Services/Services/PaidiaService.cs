using AutoMapper;
using Azure;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.Services.Services;

public class PaidiaService : IPaidiaService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse>
{
    private readonly ILogger<PaidiaService> _logger;
    private readonly IPaidiaRepository _paidiRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<PaidiDtoBase> _paidiValidator;

    public PaidiaService(
        IPaidiaRepository paidiRepository,
        IMapper mapper,
        ILogger<PaidiaService> logger,
        IValidator<PaidiDtoBase> paidiValidator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _paidiValidator = paidiValidator ?? throw new ArgumentNullException(nameof(paidiValidator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _paidiRepository = paidiRepository ?? throw new ArgumentNullException(nameof(paidiRepository));
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaByNameInService(string name, PaidiQueryParameters paidiQueryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaByNameFromDb(name, paidiQueryParameters);
        if (paidia == null)
            return null!;

        var paidiaResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (paidiaResponse == null)
            return null!;
        return paidiaResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaByKoinotitaIdInService(int id, PaidiQueryParameters paidiQueryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInKoinotitaIdFromDb(id, paidiQueryParameters);
        if (paidia == null)
            return null!;

        var paidiaResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (paidiaResponse == null)
            return null!;
        return paidiaResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaByKoinotitaNameInService(string koinotitaName, PaidiQueryParameters paidiQueryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInKoinotitaNameFromDb(koinotitaName, paidiQueryParameters);
        if (paidia == null)
            return null!;

        var paidiaResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (paidiaResponse == null)
            return null!;
        return paidiaResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaBySkiniInService(string skini, PaidiQueryParameters paidiQueryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInSkiniFromDb(skini, paidiQueryParameters);
        if (paidia == null)
            return null!;

        var kataskinotes = paidia.OfType<Kataskinotis>().ToList();
        var kataskinotesResponse = _mapper.Map<IEnumerable<PaidiResponse>>(kataskinotes);
        if (kataskinotesResponse == null)
            return null!;
        return kataskinotesResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaBySkiniIdInService(int skiniId, PaidiQueryParameters paidiQueryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInSkiniIdFromDb(skiniId, paidiQueryParameters);
        var kataskinotesResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);

        return kataskinotesResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaBySxoliInService(PaidiQueryParameters queryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInSxoliFromDb(queryParameters);
        if (paidia == null)
            return null!;

        var kataskinotesResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (kataskinotesResponse == null)
            return null!;
        return kataskinotesResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaInService(PaidiType? paidiType, PaidiQueryParameters queryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaFromDb(paidiType, queryParameters);
        if (paidia == null)
            return null!;

        var paidiaresponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (paidiaresponse == null)
            return null!;
        return paidiaresponse;
    }

    public async Task<PaidiResponse> GetPaidiByIdInService(int id, PaidiQueryParameters queryParameters)
    {
        if (_mapper == null || _paidiRepository is null)
            return null!;

        Paidi paidi = await _paidiRepository.GetPaidiByIdFromDb(id, queryParameters);
        if (paidi == null)
            return null!;

        return _mapper.Map<PaidiResponse>(paidi);
    }

    public async Task<PaidiResponse> GetPaidiByNameInService(string name, PaidiQueryParameters queryParameters)
    {
        if (_mapper == null || _paidiRepository is null)
            return null!;

        Paidi paidi = await _paidiRepository.GetPaidiByNameFromDb(name, queryParameters);
        if (paidi == null)
            return null!;

        return _mapper.Map<PaidiResponse>(paidi);
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



    public async Task<bool> CreatePaidiInService(CreatePaidiRequest paidiDto)
    {
        if (_paidiValidator == null || _mapper == null || _paidiRepository == null || string.IsNullOrEmpty(paidiDto.SkiniName))
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

            Paidi paidi = paidiDto.PaidiType switch
            {
                PaidiType.Kataskinotis => _mapper.Map<Kataskinotis>(paidiDto),
                PaidiType.Ekpaideuomenos => _mapper.Map<Ekpaideuomenos>(paidiDto),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (paidi == null)
                return false;

            var res = await _paidiRepository.AddPaidiInSkini(paidi, paidiDto.SkiniName);
            return res;
        }
    }

    public async Task<bool> DeletePaidiInService(int id)
    {
        if (_mapper == null || _paidiRepository is null)
            return false;

        var paidi = await _paidiRepository.GetPaidiByIdFromDb(id, new PaidiQueryParameters { IncludeSkini = true });
        if (paidi == null)
            return false;

        return await _paidiRepository.DeletePaidiInDb(id);
    }

    public async Task<bool> UpdatePaidiInService(UpdatePaidiRequest paidiDto)
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
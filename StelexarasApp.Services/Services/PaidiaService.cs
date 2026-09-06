using AutoMapper;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.Services.Services;

public class PaidiaService : IPaidiaService
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
            return Enumerable.Empty<PaidiResponse>();

        var paidiaResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (paidiaResponse == null)
            return null!;
        return paidiaResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaByKoinotitaIdInService(int id, PaidiQueryParameters paidiQueryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInKoinotitaIdFromDb(id, paidiQueryParameters);
        if (paidia == null)
            return Enumerable.Empty<PaidiResponse>();

        var paidiaResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (paidiaResponse == null)
            return null!;
        return paidiaResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaByKoinotitaNameInService(string koinotitaName, PaidiQueryParameters paidiQueryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInKoinotitaNameFromDb(koinotitaName, paidiQueryParameters);
        if (paidia == null)
            return Enumerable.Empty<PaidiResponse>();

        var paidiaResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (paidiaResponse == null)
            return null!;
        return paidiaResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaBySkiniInService(string skini, PaidiQueryParameters paidiQueryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInSkiniFromDb(skini, paidiQueryParameters);
        if (paidia == null)
            return Enumerable.Empty<PaidiResponse>();

        var kataskinotes = paidia.OfType<Kataskinotis>().ToList();
        var kataskinotesResponse = _mapper.Map<IEnumerable<PaidiResponse>>(kataskinotes);
        if (kataskinotesResponse == null)
            return null!;
        return kataskinotesResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaBySkiniIdInService(int skiniId, PaidiQueryParameters paidiQueryParameters)
    {
        if (skiniId <= 0)
            return Enumerable.Empty<PaidiResponse>();

        var paidia = await _paidiRepository.GetPaidiaInSkiniIdFromDb(skiniId, paidiQueryParameters);
        if (paidia is null)
            return Enumerable.Empty<PaidiResponse>();

        var kataskinotesResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (kataskinotesResponse is null)
            return null!;

        return kataskinotesResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaBySxoliInService(PaidiQueryParameters queryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaInSxoliFromDb(queryParameters);
        if (paidia == null)
            return Enumerable.Empty<PaidiResponse>();

        var kataskinotesResponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (kataskinotesResponse == null)
            return null!;
        return kataskinotesResponse;
    }

    public async Task<IEnumerable<PaidiResponse>> GetPaidiaInService(PaidiType? paidiType, PaidiQueryParameters queryParameters)
    {
        var paidia = await _paidiRepository.GetPaidiaFromDb(paidiType, queryParameters);
        if (paidia == null)
            return Enumerable.Empty<PaidiResponse>();

        var paidiaresponse = _mapper.Map<IEnumerable<PaidiResponse>>(paidia);
        if (paidiaresponse == null)
            return null!;
        return paidiaresponse;
    }

    public async Task<Result<PaidiResponse>> GetPaidiByIdInService(int id, PaidiQueryParameters queryParameters)
    {
        if (id <= 0)
            return Result.Fail<PaidiResponse>("Invalid Paidi id");

        var paidi = await _paidiRepository.GetPaidiByIdFromDb(id, queryParameters);
        if (paidi == null)
            return Result.Fail<PaidiResponse>("Paidi not found");

        return Result.Ok(_mapper.Map<PaidiResponse>(paidi));
    }

    public async Task<Result<PaidiResponse>> GetPaidiByNameInService(string name, PaidiQueryParameters queryParameters)
    {
        Paidi paidi = await _paidiRepository.GetPaidiByNameFromDb(name, queryParameters);
        if (paidi == null)
            return Result.Fail<PaidiResponse>("Paidi not found");

        return Result.Ok(_mapper.Map<PaidiResponse>(paidi));
    }

    public async Task<Result> CreatePaidiInService(CreatePaidiRequest paidiDto)
    {
        if (paidiDto is null)
            return Result.Fail("Invalid Paidi data");
        if (string.IsNullOrEmpty(paidiDto.SkiniName))
            return Result.Fail("Skini name is required");

        var validationResult = await _paidiValidator.ValidateAsync(paidiDto);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.Errors.Select(x => x.ErrorMessage));
        }

        Paidi paidi = paidiDto.PaidiType switch
        {
            PaidiType.Kataskinotis => _mapper.Map<Kataskinotis>(paidiDto),
            PaidiType.Ekpaideuomenos => _mapper.Map<Ekpaideuomenos>(paidiDto),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (paidi == null)
            return Result.Fail("Failed to create Paidi");

        var res = await _paidiRepository.AddPaidiInSkini(paidi, paidiDto.SkiniName);
        if (!res)
            return Result.Fail("Failed to create Paidi");

        return Result.Ok();
    }

    public async Task<Result> DeletePaidiInService(int id)
    {
        var paidi = await _paidiRepository.GetPaidiByIdFromDb(id, new PaidiQueryParameters { IncludeSkini = true });
        if (paidi == null)
            return Result.Fail("Paidi not found");

        var res = await _paidiRepository.DeletePaidiInDb(id);
        if (!res)
            return Result.Fail("Failed to delete Paidi");

        return Result.Ok();
    }

    public async Task<Result> UpdatePaidiInService(int id, UpdatePaidiRequest paidiDto)
    {
        if (paidiDto == null)
            return Result.Fail("PaidiDto was null");

        var validationResult = await _paidiValidator.ValidateAsync(paidiDto);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.Errors.Select(x => x.ErrorMessage));

        var skini = await _paidiRepository.GetPaidiSkiniByNameIdFromDb(paidiDto.SkiniName);
        paidiDto.SkiniName = skini.Name;
        var paidi = _mapper.Map<Paidi>(paidiDto);
        paidi.SkiniId = skini.Id;
        var result = await _paidiRepository.UpdatePaidiInDb(id, paidi);

        if (!result)
            return Result.Fail("Failed to update Paidi");
        return Result.Ok();
    }
}
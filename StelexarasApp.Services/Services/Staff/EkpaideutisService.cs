using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.Services.Services.Staff;

public class EkpaideutisService : IEkpaideutisService
{
    private readonly IStaffRepository _ekpaideutisRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<IStelexosDto> _stelexosValidator;

    public EkpaideutisService(IStaffRepository ekpaideutisRepository,
                              IMapper mapper,
                              ILogger<EkpaideutisService> logger,
                              IValidator<IStelexosDto> validator)
    {
        _ekpaideutisRepository = ekpaideutisRepository;
        _mapper = mapper;
        _stelexosValidator = validator;
    }

    public async Task<IEnumerable<EkpaideutisResponse>> GetAllEkpaideutesInService()
    {
        if (_ekpaideutisRepository is null || _mapper is null)
            throw new ArgumentException("StaffRepository or _mapper cannot be null");

        var ekpaideutisInDb = await _ekpaideutisRepository.GetStelexoiAnaXwroInDb(Thesi.Ekpaideutis, string.Empty, new());
        var ekpaideutisInService = _mapper.Map<IEnumerable<EkpaideutisResponse>>(ekpaideutisInDb);

        if (ekpaideutisInService is null || !ekpaideutisInService.Any())
            return null!;
        return ekpaideutisInService;
    }

    public async Task<EkpaideutisResponse> GetEkpaideutis(string tomeaName)
    {
        if (_ekpaideutisRepository is null || _mapper is null)
            throw new ArgumentException("StaffRepository or _mapper cannot be null");
        var ekpaideutisInDb = await _ekpaideutisRepository.GetStelexoiAnaXwroInDb(Thesi.Ekpaideutis, tomeaName, new());
        var ekpaideutisInService = _mapper.Map<EkpaideutisResponse>(ekpaideutisInDb);
        if (ekpaideutisInService is null)
            return null!;
        return ekpaideutisInService;
    }

    public async Task<bool> CreateEkpaideutisInService(CreateEkpaideutisRequest request)
    {
        var ekpaideutisToCreate = _mapper.Map<Ekpaideutis>(request);
        if (ekpaideutisToCreate is null)
            return false;
        var result = await _ekpaideutisRepository.AddStelexosInDb(ekpaideutisToCreate);
        return result;
    }

    public async Task<bool> DeleteEkpaideutisInService(DeleteEkpaideutisRequest request)
    {
        var ekpaideutisInDb = await _ekpaideutisRepository.GetStelexosByIdInDb(request.Id);
        if (ekpaideutisInDb is null)
            return false;
        var result = await _ekpaideutisRepository.DeleteStelexosInDb(ekpaideutisInDb.Id);
        return result;
    }

    public async Task<bool> UpdateEkpaideutisInService(int id, UpdateEkpaideutisRequest request)
    {
        var ekpaideutisInDb = await _ekpaideutisRepository.GetStelexosByIdInDb(id);
        if (ekpaideutisInDb is null)
            return false;
        var ekpaideutisToUpdate = _mapper.Map<Ekpaideutis>(request);
        if (ekpaideutisToUpdate is null)
            return false;
        ekpaideutisToUpdate.Id = id;
        var result = await _ekpaideutisRepository.UpdateStelexosInDb(ekpaideutisToUpdate.Id, ekpaideutisInDb);
        return result;
    }
}

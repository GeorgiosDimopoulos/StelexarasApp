using AutoMapper;
using FluentValidation;

namespace StelexarasApp.Services.Services.Staff;

public class KoinotarxisService : IKoinotarxisService
{
    private readonly IStaffRepository _stelexiRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<IStelexosDto> _stelexosValidator;

    public KoinotarxisService(
        IMapper mapper,
        IStaffRepository stelexiRepository,
        IValidator<IStelexosDto> stelexosValidator)
    {
        _stelexosValidator = stelexosValidator;        
        _stelexiRepository = stelexiRepository;
        _mapper = mapper;
    }

    public async Task<bool> CreateKoinotarxisInService(CreateKoinotarxisRequest koinotarxisDto)
    {
        var omadarxisToCreate = _mapper.Map<Koinotarxis>(koinotarxisDto);
        if (omadarxisToCreate is null)
            return false;

        var result = await _stelexiRepository.AddStelexosInDb(omadarxisToCreate);
        return result;
    }

    public async Task<bool> DeleteKoinotarxisByIdInService(int id)
    {
        var koinotarxisInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
        if (koinotarxisInDb is null)
            return false;
        var result = await _stelexiRepository.DeleteStelexosInDb(koinotarxisInDb.Id);
        return result;
    }

    public async Task<IEnumerable<KoinotarxisResponse>> GetAllKoinotarxesInService(KoinotarxisQueryParameters queryParameters)
    {
        if (_stelexiRepository is null || _mapper is null)
            throw new ArgumentException("StaffRepository or _mapper cannot be null");
        var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty, queryParameters);
        var stelexoiInService = _mapper.Map<IEnumerable<KoinotarxisResponse>>(stelexoiInDb);
        return stelexoiInService;
    }

    public async Task<IEnumerable<KoinotarxisResponse>> GetKoinotarxesSeTomeaInService(string tomeaName, KoinotarxisQueryParameters queryParameters)
    {
        if (_stelexiRepository is null || _mapper is null)
            throw new ArgumentException("StaffRepository or _mapper cannot be null");
        var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, tomeaName, queryParameters);
        var stelexoiInService = _mapper.Map<IEnumerable<KoinotarxisResponse>>(stelexoiInDb);
        return stelexoiInService;
    }

    public async Task<KoinotarxisResponse> GetKoinotarxisByIdInService(int id, KoinotarxisQueryParameters queryParameters)
    {
        if (_stelexiRepository is null || _mapper is null)
            throw new ArgumentException("StaffRepository or _mapper cannot be null");
        var stelexoiInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
        if (stelexoiInDb is null)
            return null!;
        var stelexoiInService = _mapper.Map<KoinotarxisResponse>(stelexoiInDb);
        return stelexoiInService;
    }

    public async Task<KoinotarxisResponse> GetKoinotarxisByNameInService(string name, KoinotarxisQueryParameters queryParameters)
    {
        if (_stelexiRepository is null || _mapper is null)
            throw new ArgumentException("StaffRepository or _mapper cannot be null");
        
        var stelexoiInDb = await _stelexiRepository.GetStelexosByNameInDb(name, Thesi.Koinotarxis, queryParameters);
        if (stelexoiInDb is null)
            return null!;
        
        var stelexoiInService = _mapper.Map<KoinotarxisResponse>(stelexoiInDb);
        return stelexoiInService;
    }

    public async Task<bool> UpdateKoinotarxisInService(int id, UpdateKoinotarxisRequest koinotarxisDto)
    {
        var koinotarxisInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
        if (koinotarxisInDb is null)
            return false;
        var koinotarxisToUpdate = _mapper.Map<Koinotarxis>(koinotarxisDto);
        
        if (koinotarxisToUpdate is null)
            return false;
        
        koinotarxisToUpdate.Id = id;        
        var result = await _stelexiRepository.UpdateStelexosInDb(koinotarxisToUpdate.Id, koinotarxisInDb);
        return result;
    }
}
using AutoMapper;

namespace StelexarasApp.Services.Services.Staff;

public class OmadarxisService : IOmadarxisService
{
    private readonly IStaffRepository _stelexiRepository;
    private readonly IMapper _mapper;

    public OmadarxisService(IStaffRepository stelexiRepository, IMapper mapper)
    {
        _stelexiRepository = stelexiRepository;
        _mapper = mapper;
    }

    public async Task<bool> CreateOmadarxisInService(CreateOmadarxisRequest omadarxisDto)
    {
        var omadarxisToCreate = _mapper.Map<Omadarxis>(omadarxisDto);
        if (omadarxisToCreate is null)
            return false;

        var result = await _stelexiRepository.AddStelexosInDb(omadarxisToCreate);
        return result;
    }

    public async Task<bool> DeleteOmadarxisInService(DeleteOmadarxisRequest request)
    {
        var omadarxisInDb = await _stelexiRepository.GetStelexosByIdInDb(request.Id);
        if (omadarxisInDb is null)
            return false;

        var result = await _stelexiRepository.DeleteStelexosInDb(omadarxisInDb.Id);
        return result;
    }

    public async Task<IEnumerable<OmadarxisResponse>> GetAllOmadarxesInService(OmadarxisQueryParameters queryParameters)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, string.Empty, queryParameters);
            var stelexoiInService = _mapper.Map<IEnumerable<OmadarxisResponse>>(stelexoiInDb);
            return stelexoiInService;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    public async Task<IEnumerable<OmadarxisResponse>> GetOmadarxesSeKoinotitaInService(string name, OmadarxisQueryParameters queryParameters)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");
            var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, name, queryParameters);
            if (stelexoiInDb is null || !stelexoiInDb.Any())
                return [];
            var omadarxesDto = _mapper.Map<IEnumerable<OmadarxisResponse>>(stelexoiInDb);
            return omadarxesDto;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return [];
        }
    }

    public async Task<IEnumerable<OmadarxisResponse>> GetOmadarxesSeTomeaInService(string tomeaDtoName, OmadarxisQueryParameters omadarxisQueryParameters)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, tomeaDtoName, omadarxisQueryParameters);
            if (stelexoiInDb is null || !stelexoiInDb.Any())
                return [];

            var omadarxesDto = _mapper.Map<IEnumerable<OmadarxisResponse>>(stelexoiInDb);
            return omadarxesDto;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return [];
        }
    }

    public async Task<OmadarxisResponse> GetOmadarxisByIdInService(int id)
    {
        var omadarxisInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
        if (omadarxisInDb is null)
            return null!;

        var omadarxisDto = _mapper.Map<OmadarxisResponse>(omadarxisInDb);
        return omadarxisDto;
    }


    public async Task<bool> MoveOmadarxisToAnotherSkiniInService(int Id, string newSkiniName)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null || Id is <= 0 || string.IsNullOrEmpty(newSkiniName))
                throw new ArgumentException("StaffRepository, Ids or _mapper cannot be null");

            var omadarxis = await _stelexiRepository.GetStelexosByIdInDb(Id);
            if (omadarxis == null)
                return false;

            return await _stelexiRepository.MoveOmadarxisToAnotherSkiniInDb(omadarxis.Id, newSkiniName);
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return false;
        }
    }

    public async Task<bool> UpdateOmadarxisInService(UpdateOmadarxisRequest omadarxisDto)
    {
        var omadarxisInDb = await _stelexiRepository.GetStelexosByIdInDb(omadarxisDto.Id);
        if (omadarxisInDb is null)
            return false;

        var omadarxis = _mapper.Map<Omadarxis>(omadarxisDto);
        if (omadarxis is null)
            return false;

        omadarxisInDb = _mapper.Map(omadarxis, omadarxisInDb);
        var result = await _stelexiRepository.UpdateStelexosInDb(omadarxisInDb.Id, omadarxisInDb);
        return result;
    }
}

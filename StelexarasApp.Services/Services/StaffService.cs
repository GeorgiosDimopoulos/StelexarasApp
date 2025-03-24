using AutoMapper;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Library.Models.Logs;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;
using FluentValidation;

namespace StelexarasApp.Services.Services;

public class StaffService : IStaffService
{
    private readonly IStaffRepository? _stelexiRepository;
    private readonly IMapper? _mapper;
    private readonly IValidator<IStelexosDto> _stelexosValidator;

    public StaffService(IMapper mapper, IStaffRepository stelexiRepository, IValidator<IStelexosDto> stelexosValidator)
    {
        _stelexosValidator = stelexosValidator;
        _mapper = mapper;
        _stelexiRepository = stelexiRepository;
    }

    public async Task<IEnumerable<EkpaideutisDto>> GetAllEkpaideutesInService() 
    {
        if (_stelexiRepository is null || _mapper is null)
            throw new ArgumentException("StaffRepository or _mapper cannot be null");

        var ekpaideutisInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Ekpaideutis, string.Empty);
        var ekpaideutisInService = _mapper.Map<IEnumerable<EkpaideutisDto>>(ekpaideutisInDb);

        if (ekpaideutisInService is null || !ekpaideutisInService.Any())
            return null!;
        return ekpaideutisInService;
    }

    public async Task<IEnumerable<IStelexosDto>> GetAllStaffInService()
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var omadarxesInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, string.Empty);
            var koinotarxesInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty);
            var tomearxesInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Tomearxis, string.Empty);
            var ekpaiduetesInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Ekpaideutis, string.Empty);

            var allStelexoiInDb = omadarxesInDb.Concat(koinotarxesInDb ?? []).Concat(tomearxesInDb ?? []).Concat(ekpaiduetesInDb ?? []);
            var stelexoiInService = _mapper.Map<IEnumerable<IStelexosDto>>(allStelexoiInDb);

            if (stelexoiInService is null || !stelexoiInService.Any())
                return null!;
            return stelexoiInService;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    public async Task<bool> AddStelexosInService(IStelexosDto stelexosDto)
    {
        try
        {
            if (stelexosDto == null)
                throw new ArgumentNullException(nameof(stelexosDto), "StelexosDto cannot be null");

            if (_mapper == null || _stelexiRepository == null)
                throw new InvalidOperationException("Mapper or repository cannot be null");

            var stelexosResult = await _stelexosValidator.ValidateAsync(stelexosDto);

            if (!stelexosResult.IsValid)
            {
                LogFileWriter.WriteToLog("StelexosDto is not valid to be added", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                return false;
            }

            IStelexos stelexosEntity;
            switch (stelexosDto.Thesi)
            {
                case Thesi.Omadarxis:
                    stelexosEntity = _mapper.Map<Omadarxis>(stelexosDto as OmadarxisDto);
                    break;
                case Thesi.Koinotarxis:
                    stelexosEntity = _mapper.Map<Koinotarxis>(stelexosDto as KoinotarxisDto);
                    break;
                case Thesi.Tomearxis:
                    stelexosEntity = _mapper.Map<Tomearxis>(stelexosDto as TomearxisDto);
                    break;
                case Thesi.Ekpaideutis:
                    stelexosEntity = _mapper.Map<Ekpaideutis>(stelexosDto as EkpaideutisDto);
                    break;
                case Thesi.None:
                    throw new ArgumentException("Thesi cannot be None!", nameof(stelexosDto.Thesi));
                default:
                    stelexosEntity = _mapper.Map<IStelexos>(stelexosDto);
                    break;
            }

            if (stelexosEntity == null)
            {
                LogFileWriter.WriteToLog("stelexosEntity is null", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                throw new ArgumentNullException(nameof(stelexosEntity), "Mapping failed");
            }

            return await _stelexiRepository.AddStelexosInDb(stelexosEntity);
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return false;
        }
    }

    public async Task<bool> DeleteStelexosByIdInService(int id)
    {
        return await _stelexiRepository!.DeleteStelexosInDb(id);
    }

    public async Task<IEnumerable<OmadarxisDto>> GetAllOmadarxesInService()
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, string.Empty);
            var stelexoiInService = _mapper.Map<IEnumerable<OmadarxisDto>>(stelexoiInDb);
            return stelexoiInService;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    public async Task<IEnumerable<KoinotarxisDto>> GetAllKoinotarxesInService()
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new InvalidOperationException("StaffRepository or _mapper cannot be null");

            var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty);
            if (stelexoiInDb is null || !stelexoiInDb.Any())
                return []!;

            var stelexoiInService = _mapper.Map<IEnumerable<KoinotarxisDto>>(stelexoiInDb);
            return stelexoiInService;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    public async Task<IStelexosDto> GetStelexosByNameInService(string name, Thesi? thesi)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new InvalidOperationException("StaffRepository or _mapper cannot be null");

            var stelexosInDb = await _stelexiRepository.GetStelexosByNameInDb(name, thesi);
            var stelexosInService = _mapper.Map<IStelexosDto>(stelexosInDb);
            return stelexosInService;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    public async Task<IEnumerable<TomearxisDto>> GetAllTomearxesInService()
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Tomearxis, string.Empty);
            var stelexoiInService = _mapper.Map<IEnumerable<TomearxisDto>>(stelexoiInDb);
            return stelexoiInService;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotitaInService(KoinotitaDto koinotita)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, koinotita.Name);
            if (stelexoiInDb is null || !stelexoiInDb.Any())
                return [];

            var omadarxesDto = _mapper.Map<IEnumerable<OmadarxisDto>>(stelexoiInDb);
            return omadarxesDto;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Enumerable.Empty<OmadarxisDto>();
        }
    }

    public async Task<IEnumerable<Ekpaideutis>> GetEkpaideutes()
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var ekpaideutesInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Ekpaideutis, string.Empty);
            if (ekpaideutesInDb is null || !ekpaideutesInDb.Any())
                return null!;

            return (IEnumerable<Ekpaideutis>)ekpaideutesInDb;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeTomeaInService(TomeasDto tomeaDto)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var stelexoiInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, tomeaDto.Name);
            if (stelexoiInDb is null || !stelexoiInDb.Any())
                return [];

            var omadarxesDto = _mapper.Map<IEnumerable<OmadarxisDto>>(stelexoiInDb);
            return omadarxesDto;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return [];
        }
    }

    public async Task<IStelexosDto> GetStelexosByIdInService(int id)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or mapper cannot be null");

            var stelexosInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
            if (stelexosInDb is null)
                return null!;

            var stelexosInService = _mapper.Map<IStelexosDto>(stelexosInDb);
            return stelexosInService;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
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

    public async Task<bool> UpdateStelexosInService(int id, IStelexosDto stelexosDto)
    {
        if (_stelexiRepository is null || stelexosDto is null)
            throw new ArgumentException("StaffRepository or stelexosDto cannot be null");

        var stelexos = MapDtoToEntity(stelexosDto);
        if (stelexos == null)
            return false;

        return await _stelexiRepository.UpdateStelexosInDb(id, stelexos);
    }

    public async Task<IEnumerable<KoinotarxisDto>> GetKoinotarxesSeTomeaInService(TomeasDto tomea)
    {
        try
        {
            if (_stelexiRepository is null || _mapper is null)
                throw new ArgumentException("StaffRepository or _mapper cannot be null");

            var koinotarxes = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, tomea.Name);
            if (koinotarxes is null || !koinotarxes.Any())
                return [];

            var koinotarxesDto = _mapper.Map<IEnumerable<KoinotarxisDto>>(koinotarxes);

            return koinotarxesDto;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    private IStelexos MapDtoToEntity(IStelexosDto stelexosDto)
    {
        if (stelexosDto is null)
            return null!;

        return stelexosDto.Thesi switch
        {
            Thesi.Omadarxis => _mapper!.Map<Omadarxis>(stelexosDto),
            Thesi.Koinotarxis => _mapper!.Map<Koinotarxis>(stelexosDto),
            Thesi.Tomearxis => _mapper!.Map<Tomearxis>(stelexosDto),
            _ => _mapper!.Map<IStelexos>(stelexosDto),
        };
    }
}

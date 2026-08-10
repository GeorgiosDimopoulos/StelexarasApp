using AutoMapper;
using FluentValidation;

namespace StelexarasApp.Services.Services;

public class StaffService : IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse>
{
    private readonly IStaffRepository _stelexiRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<StelexosDtoBase> _stelexosValidator;

    public StaffService(IMapper mapper, IStaffRepository stelexiRepository, IValidator<StelexosDtoBase> stelexosValidator)
    {
        _stelexosValidator = stelexosValidator;
        _mapper = mapper;
        _stelexiRepository = stelexiRepository;
    }

    public async Task<bool> CreateStelexos(CreateStelexosRequest stelexosDto, Thesi thesi)
    {
        try
        {
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
                    stelexosEntity = _mapper.Map<Omadarxis>(stelexosDto);
                    break;
                case Thesi.Koinotarxis:
                    stelexosEntity = _mapper.Map<Koinotarxis>(stelexosDto);
                    break;
                case Thesi.Tomearxis:
                    stelexosEntity = _mapper.Map<Tomearxis>(stelexosDto);
                    break;
                case Thesi.Ekpaideutis:
                    stelexosEntity = _mapper.Map<Ekpaideutis>(stelexosDto);
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

    public async Task<bool> DeleteStelexos(int id)
    {
        return await _stelexiRepository.DeleteStelexosInDb(id);
    }

    public async Task<IEnumerable<StelexosResponse>> GetStelexi(Thesi thesi, string? xwros, StelexosQueryParameters? stelexosQueryParameters)
    {
        var stelexosInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(thesi, xwros, stelexosQueryParameters);
        if (stelexosInDb is null)
            return null!;
        var stelexosInService = _mapper.Map<IEnumerable<StelexosResponse>>(stelexosInDb);
        return stelexosInService;
    }

    public async Task<StelexosResponse> GetStelexosById(int id, StelexosQueryParameters stelexosQueryParameters)
    {
        var stelexosInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
        if (stelexosInDb is null)
            return null!;

        var stelexosInService = _mapper.Map<StelexosResponse>(stelexosInDb);
        return stelexosInService;
    }

    public async Task<StelexosResponse> GetStelexosByName(Thesi thesi, string n, StelexosQueryParameters stelexosQueryParameters)
    {
        var stelexosInDb = await _stelexiRepository.GetStelexosByNameInDb(n, thesi, stelexosQueryParameters);
        if (stelexosInDb is null)
            return null!;

        var stelexosInService = _mapper.Map<StelexosResponse>(stelexosInDb);
        return stelexosInService;
    }

    public async Task<bool> UpdateStelexos(int id, UpdateStelexosRequest entity)
    {
        _stelexosValidator.ValidateAndThrow(entity);
        var stelexosInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
        if (stelexosInDb == null)
        {
            LogFileWriter.WriteToLog("Stelexos not found", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return false;
        }
        var stelexosEntity = _mapper.Map(entity, stelexosInDb);
        var result = await _stelexiRepository.UpdateStelexosInDb(id, stelexosEntity);
        if (result)
        {
            LogFileWriter.WriteToLog("Stelexos updated successfully", System.Reflection.MethodBase.GetCurrentMethod()!.Name, CrudType.Update);
            return true;
        }
        else
        {
            LogFileWriter.WriteToLog("Failed to update Stelexos", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return false;
        }
    }

    public Task<bool> MoveOmadarxisToAnotherSkiniInService(int id, string skiniName)
    {
        return _stelexiRepository.MoveOmadarxisToAnotherSkiniInDb(id, skiniName);
    }
}

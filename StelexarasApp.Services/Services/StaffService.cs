using AutoMapper;
using FluentResults;
using FluentValidation;

namespace StelexarasApp.Services.Services;

public class StaffService : IStaffService
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

    public async Task<IEnumerable<StelexosResponse>> GetStelexi(Thesi? thesi, StelexosQueryParameters? stelexosQueryParameters)
    {
        var stelexosInDb = await _stelexiRepository.GetStelexiInDb(thesi, stelexosQueryParameters);
        if (stelexosInDb is null)
            return new List<StelexosResponse>();
        var stelexosInService = _mapper.Map<IEnumerable<StelexosResponse>>(stelexosInDb);
        if (!stelexosInService.Any())
        {
            LogFileWriter.WriteToLog("No stelexos found", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return new List<StelexosResponse>();
        }
        return stelexosInService;
    }
        
    public async Task<IEnumerable<StelexosResponse>> GetStelexoiAnaXwro(string? xwros, StelexosQueryParameters? stelexosQueryParameters)
    {
        var stelexosInDb = await _stelexiRepository.GetStelexoiAnaXwroInDb(xwros, stelexosQueryParameters);
        if (stelexosInDb is null)
            return new List<StelexosResponse>();
        var stelexosInService = _mapper.Map<IEnumerable<StelexosResponse>>(stelexosInDb);
        if (stelexosInService is null)
        {
            LogFileWriter.WriteToLog("No stelexos found", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return new List<StelexosResponse>();
        }
        return stelexosInService;
    }

    public async Task<Result<StelexosResponse>> GetStelexosById(int id, StelexosQueryParameters stelexosQueryParameters)
    {
        var stelexosInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
        if (stelexosInDb is null)
            return Result.Fail("Stelexos not found");

        var stelexosInService = _mapper.Map<StelexosResponse>(stelexosInDb);
        if (stelexosInService is null)
        {
            LogFileWriter.WriteToLog("Stelexos not found", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Stelexos not found");
        }
        return Result.Ok(stelexosInService);
    }

    public async Task<Result<StelexosResponse>> GetStelexosByName(string n, StelexosQueryParameters stelexosQueryParameters)
    {
        var stelexosInDb = await _stelexiRepository.GetStelexosByNameInDb(n, stelexosQueryParameters);
        if (stelexosInDb is null)
            return Result.Fail("Stelexos not found");

        var stelexosInService = _mapper.Map<StelexosResponse>(stelexosInDb);
        if (stelexosInService is null)
        {
            LogFileWriter.WriteToLog("Stelexos not found", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Stelexos not found");
        }
        return Result.Ok(stelexosInService);
    }


    public async Task<Result> CreateStelexos(CreateStelexosRequest stelexosDto)
    {
        var stelexosResult = await _stelexosValidator.ValidateAsync(stelexosDto);

        if (!stelexosResult.IsValid)
        {
            LogFileWriter.WriteToLog("StelexosDto is not valid to be added", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("StelexosDto is not valid to be added");
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
            ArgumentNullException argumentNullException = new(nameof(stelexosEntity), "Mapping failed");
            throw argumentNullException;
        }

        var result = await _stelexiRepository.AddStelexosInDb(stelexosEntity);
        if (result)
        {
            LogFileWriter.WriteToLog("Stelexos added successfully", System.Reflection.MethodBase.GetCurrentMethod()!.Name, CrudType.Create);
            return Result.Ok();
        }
        else
        {
            LogFileWriter.WriteToLog("Failed to add Stelexos", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to add Stelexos");
        }
    }

    public async Task<Result> DeleteStelexos(int id)
    {
        var result = await _stelexiRepository.DeleteStelexosInDb(id);
        if (result)
        {
            LogFileWriter.WriteToLog("Stelexos deleted successfully", System.Reflection.MethodBase.GetCurrentMethod()!.Name, CrudType.Delete);
            return Result.Ok();
        }
        else
        {
            LogFileWriter.WriteToLog("Failed to delete Stelexos", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to delete Stelexos");
        }
    }

    public async Task<Result> UpdateStelexos(int id, UpdateStelexosRequest entity)
    {
        _stelexosValidator.ValidateAndThrow(entity);
        var stelexosInDb = await _stelexiRepository.GetStelexosByIdInDb(id);
        if (stelexosInDb == null)
        {
            LogFileWriter.WriteToLog("Stelexos not found", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Stelexos not found");
        }
        var stelexosEntity = _mapper.Map(entity, stelexosInDb);
        var result = await _stelexiRepository.UpdateStelexosInDb(id, stelexosEntity);
        if (result)
        {
            LogFileWriter.WriteToLog("Stelexos updated successfully", System.Reflection.MethodBase.GetCurrentMethod()!.Name, CrudType.Update);
            return Result.Ok();
        }
        else
        {
            LogFileWriter.WriteToLog("Failed to update Stelexos", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to update Stelexos");
        }
    }

    public async Task<Result> MoveOmadarxisToAnotherSkiniInService(int id, string skiniName)
    {
        var result = await _stelexiRepository.MoveOmadarxisToAnotherSkiniInDb(id, skiniName);
        if (result)
        {
            LogFileWriter.WriteToLog("Omadarxis moved successfully", System.Reflection.MethodBase.GetCurrentMethod()!.Name, CrudType.Update);
            return Result.Ok();
        }
        else
        {
            LogFileWriter.WriteToLog("Failed to move Omadarxis", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to move Omadarxis");
        }
    }
}

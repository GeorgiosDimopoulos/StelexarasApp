using AutoMapper;
using FluentResults;
using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.Services.Services;

public class TeamsService(IMapper mapper, ITeamsRepository teamsRepository) : ITeamsService
{
    private readonly ITeamsRepository _teamsRepository = teamsRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> AddSkiniInService(CreateSkiniRequest skiniDto)
    {
        if (skiniDto is null || string.IsNullOrEmpty(skiniDto.Name) || skiniDto.KoinotitaId == 0)
            return Result.Fail("Invalid data for adding a Skini");

        var skini = _mapper.Map<Skini>(skiniDto);
        var skiniAdded = await _teamsRepository.AddSkiniInDb(skini);
        if (skiniAdded)
        {
            return Result.Ok();
        }
        else
        {
            //LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            LogFileWriter.WriteToLog($"Failed to add Skini with name: {skiniDto.Name} and KoinotitaId: {skiniDto.KoinotitaId}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to add Skini");
        }
    }

    public async Task<IEnumerable<SkiniResponse>> GetAllSkinesInService(SkiniQueryParameters? skiniQueryParameters)
    {
        var skini = await _teamsRepository.GetSkinesInDb(skiniQueryParameters);
        return _mapper.Map<IEnumerable<SkiniResponse>>(skini);
    }

    public async Task<Result<SkiniResponse>> GetSkiniByNameInService(SkiniQueryParameters? skiniQueryParameters, string name)
    {
        var skini = await _teamsRepository.GetSkiniByNameInDb(skiniQueryParameters, name);
        return Result.Ok(_mapper.Map<SkiniResponse>(skini));
    }

    public async Task<Result<SkiniResponse>> GetSkiniByIdInService(SkiniQueryParameters? skiniQueryParameters, int id)
    {
        var skini = await _teamsRepository.GetSkiniByIdInDb(skiniQueryParameters, id);
        if (skini is null)
            return Result.Fail<SkiniResponse>("Skini not found");

        return Result.Ok(_mapper.Map<SkiniResponse>(skini));
    }
    
    public async Task<Result<KoinotitaResponse>> GetKoinotitaByIdInService(int id, KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var skini = await _teamsRepository.GetKoinotitaByIdInDb(id, koinotitaQueryParameters);
        if (skini is null)
            return Result.Fail<KoinotitaResponse>("Koinotita not found");

        return Result.Ok(_mapper.Map<KoinotitaResponse>(skini));
    }

    public async Task<IEnumerable<SkiniResponse>> GetSkinesAnaKoinotitaIdInService(SkiniQueryParameters? skiniQueryParameters, int id)
    {
        var skines = await _teamsRepository.GetSkinesAnaKoinotitaIdInDb(skiniQueryParameters, id);
        return _mapper.Map<IEnumerable<SkiniResponse>>(skines);
    }

    public async Task<IEnumerable<SkiniResponse>> GetSkinesAnaKoinotitaNameInService(SkiniQueryParameters? skiniQueryParameters, string koinotitaName)
    {
        var skines = await _teamsRepository.GetSkinesAnaKoinotitaNameInDb(skiniQueryParameters, koinotitaName);
        return _mapper.Map<IEnumerable<SkiniResponse>>(skines);
    }

    public async Task<IEnumerable<SkiniResponse>> GetSkinesEkpaideuomenonInService(SkiniQueryParameters? skiniQueryParameters)
    {
        var skines = await _teamsRepository.GetSkinesEkpaideuomenonInDb(skiniQueryParameters);
        return _mapper.Map<IEnumerable<SkiniResponse>>(skines);
    }

    public async Task<IEnumerable<KoinotitaResponse>> GetAllKoinotitesInService(KoinotitaQueryParameters? koinotitaQueryParameters)
    {
        var koinotitaInDb = await _teamsRepository.GetKoinotitesInDb(koinotitaQueryParameters);
        return _mapper.Map<IEnumerable<KoinotitaResponse>>(koinotitaInDb);
    }

    public async Task<IEnumerable<KoinotitaResponse>> GetKoinotitesAnaTomeaInService(KoinotitaQueryParameters? koinotitaQueryParameters, int tomeaId)
    {
        var koinotitaInDb = await _teamsRepository.GetKoinotitesAnaTomeaInDb(koinotitaQueryParameters, tomeaId);
        return _mapper.Map<IEnumerable<KoinotitaResponse>>(koinotitaInDb);
    }

    public async Task<IEnumerable<TomeasResponse>> GetAllTomeisInService(TomeasQueryParameters tomeasQueryParameters)
    {
        var tomeisInDb = await _teamsRepository.GetTomeisInDb(tomeasQueryParameters);
        return _mapper.Map<IEnumerable<TomeasResponse>>(tomeisInDb);
    }

    public async Task<Result<KoinotitaResponse>> GetKoinotitaByNameInService(KoinotitaQueryParameters? koinotitaQueryParameters, string name)
    {
        var skini = await _teamsRepository.GetKoinotitaByNameInDb(koinotitaQueryParameters, name);
        return Result.Ok(_mapper.Map<KoinotitaResponse>(skini));
    }
    
    public async Task<Result<TomeasResponse>> GetTomeaByNameInService(TomeasQueryParameters tomeasQueryParameters, string name)
    {
        var tomeasInDb = await _teamsRepository.GetTomeaByNameInDb(tomeasQueryParameters, name);
        if (tomeasInDb == null)
        {
            return Result.Fail<TomeasResponse>("Tomeas not found");
        }
        return Result.Ok(_mapper.Map<TomeasResponse>(tomeasInDb));
    }

    public async Task<IEnumerable<string>> GetAnwtatoiXwroi()
    {
        var anwtatoiXwroiInDb = await _teamsRepository.GetAnwtatoiXwroiInDb();
        return anwtatoiXwroiInDb;
    }

    public async Task<Result> AddKoinotitaInService(CreateKoinotitaRequest request)
    {
        if (request is null || string.IsNullOrEmpty(request.Name))
            return Result.Fail("Invalid data for adding a Koinotita");

        var tomeasExisting = await _teamsRepository.GetTomeaByNameInDb(new(), request.TomeasName);
        if (tomeasExisting == null)
            return Result.Fail("Tomeas not found");

        var koinotita = _mapper.Map<Koinotita>(request);
        koinotita.Tomeas = tomeasExisting;

        var koinotitaAdded = await _teamsRepository.AddKoinotitaInDb(koinotita);
        if (koinotitaAdded)
            return Result.Ok();
        else
        {
            LogFileWriter.WriteToLog($"Failed to add Koinotita with name: {request.Name} and TomeasName: {request.TomeasName}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to add Koinotita");
        }        
    }

    public async Task<Result> UpdateKoinotitaInService(int id, UpdateKoinotitaRequest koinotitaDto)
    {
        var existing = await _teamsRepository.GetKoinotitaByIdInDb(id, new());
        if (existing is null)
            return Result.Fail("Koinotita not found");

        var tomeas = await _teamsRepository.GetTomeaByNameInDb(new(), koinotitaDto.TomeasName);
        if (tomeas == null)
        {
            return Result.Fail("Tomeas not found");
        }

        var koinotita = _mapper.Map<Koinotita>(koinotitaDto);
        koinotita.TomeasId = tomeas.Id;

        var koinotitaUpdated = await _teamsRepository.UpdateKoinotitaInDb(id, koinotita);
        if (koinotitaUpdated == false)
        {
            LogFileWriter.WriteToLog($"Failed to update Koinotita with id: {id} and TomeasName: {koinotitaDto.TomeasName}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to update Koinotita");
        }

        return Result.Ok();
    }

    public async Task<Result> UpdateSkiniInService(int id, UpdateSkiniRequest skiniDto)
    {
        var skini = _mapper.Map<Skini>(skiniDto);
        var skiniUpdated = await _teamsRepository.UpdateSkiniInDb(id, skini);
        if (skiniUpdated == false)
        {
            LogFileWriter.WriteToLog($"Failed to update Skini with id: {id} and Name: {skiniDto.Name}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to update Skini");
        }

        return Result.Ok();
    }

    public async Task<Result> UpdateTomeaInService(string id, UpdateTomeasRequest tomeasDto)
    {
        var tomeas = _mapper.Map<Tomeas>(tomeasDto);
        var tomeasUpdated = await _teamsRepository.UpdateTomeasInDb(id, tomeas);
        if (tomeasUpdated == false)
        {
            LogFileWriter.WriteToLog($"Failed to update Tomeas with id: {id} and Name: {tomeasDto.Name}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to update Tomeas");
        }

        return Result.Ok();
    }

    public async Task<Result> DeleteSkiniInService(int skiniId)
    {
        var deleted = await _teamsRepository.DeleteSkiniInDb(skiniId);
        if (!deleted)
        {
            LogFileWriter.WriteToLog($"Failed to delete Skini with id: {skiniId}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to delete Skini");
        }
        return Result.Ok();
    }

    public async Task<Result> DeleteKoinotitaInService(int koinotitaId)
    {
        var deleted = await _teamsRepository.DeleteKoinotitaInDb(koinotitaId);
        if (!deleted)
        {
            LogFileWriter.WriteToLog($"Failed to delete Koinotita with id: {koinotitaId}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to delete Koinotita");
        }
        return Result.Ok();
    }

    public async Task<Result> DeleteTomeasInService(string n)
    {
        var deleted = await _teamsRepository.DeleteTomeasInDb(n);
        if (!deleted)
        {
            LogFileWriter.WriteToLog($"Failed to delete Tomeas with name: {n}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return Result.Fail("Failed to delete Tomeas");
        }
        return Result.Ok();
    }

    public async Task<Result> AddTomeasInService(CreateTomeasRequest tomeasDto)
    {
        if (tomeasDto is null || string.IsNullOrEmpty(tomeasDto.Name))
            return Result.Fail("Invalid data for adding a Tomeas");
        var tomeas = _mapper.Map<Tomeas>(tomeasDto);
        var tomeasAdded = await _teamsRepository.AddTomeasInDb(tomeas);
        if (tomeasAdded)
            return Result.Ok();
        else
            return Result.Fail("Failed to add Tomeas");
    }
}

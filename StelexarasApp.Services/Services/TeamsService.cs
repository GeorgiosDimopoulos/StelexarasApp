using AutoMapper;

namespace StelexarasApp.Services.Services;

public class TeamsService(IMapper mapper, ITeamsRepository teamsRepository) : ITeamsService
{
    private readonly ITeamsRepository _teamsRepository = teamsRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> AddSkiniInService(CreateSkiniDto skiniDto)
    {
        try
        {
            if (skiniDto is null || string.IsNullOrEmpty(skiniDto.Name) || string.IsNullOrEmpty(skiniDto.KoinotitaName))
                return false;

            var skini = _mapper.Map<Skini>(skiniDto);
            return await _teamsRepository.AddSkiniInDb(skini);
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return false;
        }
    }

    public async Task<bool> AddKoinotitaInService(CreateKoinotitaRequest koinotitaDto)
    {
        try
        {
            if (koinotitaDto is null || string.IsNullOrEmpty(koinotitaDto.Name))
                return false;

            var tomeasExisting = await _teamsRepository.GetTomeaByNameInDb(new(), koinotitaDto.TomeasName);
            if (tomeasExisting == null)
            {
                return false;
            }

            var koinotita = _mapper.Map<Koinotita>(koinotitaDto);

            koinotita.Tomeas = await _teamsRepository.GetTomeaByNameInDb(new(), koinotitaDto.TomeasName);
            return await _teamsRepository.AddKoinotitaInDb(koinotita);
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return false;
        }
    }

    public async Task<IEnumerable<SkiniResponse>> GetAllSkinesInService(SkiniQueryParameters? skiniQueryParameters)
    {
        try
        {
            var skini = await _teamsRepository.GetSkinesInDb(skiniQueryParameters);
            return _mapper.Map<IEnumerable<SkiniResponse>>(skini);
        }
        catch
        {
            return new List<SkiniResponse>();
        }
    }

    public async Task<SkiniResponse> GetSkiniByNameInService(SkiniQueryParameters skiniQueryParameters, string name)
    {
        var skini = await _teamsRepository.GetSkiniByNameInDb(skiniQueryParameters, name);
        return _mapper.Map<SkiniResponse>(skini);
    }

    public async Task<IEnumerable<SkiniResponse>> GetSkinesAnaKoinotitaInService(SkiniQueryParameters? skiniQueryParameters, string koinotitaName)
    {
        var skines = await _teamsRepository.GetSkinesAnaKoinotitaInDb(skiniQueryParameters, koinotitaName);
        return _mapper.Map<IEnumerable<SkiniResponse>>(skines);
    }

    public async Task<IEnumerable<SkiniResponse>> GetSkinesEkpaideuomenonInService(SkiniQueryParameters skiniQueryParameters)
    {
        var skines = await _teamsRepository.GetSkinesEkpaideuomenonInDb(skiniQueryParameters);
        return _mapper.Map<IEnumerable<SkiniResponse>>(skines);
    }

    public async Task<IEnumerable<KoinotitaResponse>> GetAllKoinotitesInService(KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var koinotitaInDb = await _teamsRepository.GetKoinotitesInDb(koinotitaQueryParameters);
        return _mapper.Map<IEnumerable<KoinotitaResponse>>(koinotitaInDb);
    }

    public async Task<IEnumerable<KoinotitaResponse>> GetKoinotitesAnaTomeaInService(KoinotitaQueryParameters koinotitaQueryParameters, int tomeaId)
    {
        var koinotitaInDb = await _teamsRepository.GetKoinotitesAnaTomeaInDb(koinotitaQueryParameters, tomeaId);
        return _mapper.Map<IEnumerable<KoinotitaResponse>>(koinotitaInDb);
    }

    public async Task<IEnumerable<TomeasResponse>> GetAllTomeisInService(TomeasQueryParameters tomeasQueryParameters)
    {
        var tomeisInDb = await _teamsRepository.GetTomeisInDb(tomeasQueryParameters);
        return _mapper.Map<IEnumerable<TomeasResponse>>(tomeisInDb);
    }

    public async Task<KoinotitaResponse> GetKoinotitaByNameInService(KoinotitaQueryParameters koinotitaQueryParameters, string name)
    {
        var skini = await _teamsRepository.GetKoinotitaByNameInDb(koinotitaQueryParameters, name);
        return _mapper.Map<KoinotitaResponse>(skini);
    }

    public Task<bool> UpdateKoinotitaInService(int id, UpdateKoinotitaRequest koinotitaDto)
    {
        var koinotita = _mapper.Map<Koinotita>(koinotitaDto);
        return _teamsRepository.UpdateKoinotitaInDb(id, koinotita);
    }

    public Task<bool> UpdateSkiniInService(int id, UpdateSkiniDto skiniDto)
    {
        var skini = _mapper.Map<Skini>(skiniDto);
        return _teamsRepository.UpdateSkiniInDb(id, skini);
    }

    public Task<bool> UpdateTomeaInService(string id, UpdateTomeasRequest tomeasDto)
    {
        var tomeas = _mapper.Map<Tomeas>(tomeasDto);
        return _teamsRepository.UpdateTomeasInDb(id, tomeas);
    }

    public Task<bool> DeleteSkiniInService(int skiniId)
    {
        return _teamsRepository.DeleteSkiniInDb(skiniId);
    }

    public Task<bool> DeleteKoinotitaInService(int koinotitaId)
    {
        return _teamsRepository.DeleteKoinotitaInDb(koinotitaId);
    }

    public Task<bool> DeleteTomeasInService(string n)
    {
        return _teamsRepository.DeleteTomeasInDb(n);
    }

    public async Task<TomeasResponse> GetTomeaByNameInService(TomeasQueryParameters tomeasQueryParameters, string name)
    {
        var tomeasInDb = await _teamsRepository.GetTomeaByNameInDb(tomeasQueryParameters, name);
        return _mapper.Map<TomeasResponse>(tomeasInDb);
    }

    public async Task<bool> AddTomeasInService(CreateTomeasRequest tomeasDto)
    {
        try
        {
            if (tomeasDto is null || string.IsNullOrEmpty(tomeasDto.Name))
                return false;
            var tomeas = _mapper.Map<Tomeas>(tomeasDto);
            return await _teamsRepository.AddTomeasInDb(tomeas);
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return false;
        }
    }

    public Task<bool> HasData()
    {
        if (!_teamsRepository.GetSkinesInDb(new()).Result.Any() &&
            !_teamsRepository.GetKoinotitesAnaTomeaInDb(new(), 2).Result.Any() &&
            !_teamsRepository.GetKoinotitesAnaTomeaInDb(new(), 1).Result.Any())
            return Task.FromResult(false);
        return Task.FromResult(true);
    }

    public async Task<bool> CheckStelexousXwroNameInService(IStelexosDto stelexosDto, string xwrosName)
    {
        switch (stelexosDto.Thesi)
        {
            case Thesi.Omadarxis:
                var skini = await _teamsRepository.GetSkiniByNameInDb(new(), xwrosName);
                if (skini == null)
                {
                    Console.WriteLine($"Skini {xwrosName} doesnt exist in DB!");
                    return false;
                }
                return true;
            case Thesi.Koinotarxis:
                var koinotita = await _teamsRepository.GetKoinotitaByNameInDb(new(), xwrosName);
                if (koinotita == null)
                {
                    Console.WriteLine($"Koinotita {xwrosName} doesnt exist in DB!");
                    return false;
                }
                return true;
            case Thesi.Tomearxis:
                var tomeas = await _teamsRepository.GetTomeaByNameInDb(new(), xwrosName);
                if (tomeas == null)
                {
                    Console.WriteLine($"Tomeas {xwrosName} doesnt exist in DB!");
                    return false;
                }
                return true;
            case Thesi.Ekpaideutis:
                throw new NotImplementedException();
            default:
                return false;
        }
    }
}

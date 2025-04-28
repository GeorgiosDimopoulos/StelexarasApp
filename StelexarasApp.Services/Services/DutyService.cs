using AutoMapper;

namespace StelexarasApp.Services.Services;

public class DutyService : IDutyService
{
    private readonly IDutyRepository? _dutyRepository;
    private readonly IMapper _mapper = default!;

    public DutyService(IDutyRepository dutyRepository, IMapper mapper)
    {
        try
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dutyRepository = dutyRepository;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
        }
    }

    public async Task<bool> AddDutyInService(DutyDto dutyDto)
    {
        try
        {
            if (string.IsNullOrEmpty(dutyDto.Name) || _dutyRepository is null)
                throw new ArgumentException("Duty name or duty Repository cannot be null");

            var duty = _mapper.Map<Duty>(dutyDto);
            return await _dutyRepository.AddDutyInDb(duty);
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return false;
        }
    }
    public async Task<bool> DeleteDutyInService(int dutyId)
    {
        if (_dutyRepository is null)
            throw new ArgumentException("Duty name or duty Repository cannot be null");
        return await _dutyRepository.DeleteDutyInDb(dutyId);
    }

    public async Task<bool> UpdateDutyInService(string dutyName, DutyDto updatedDutyDto)
    {
        if (string.IsNullOrEmpty(dutyName) || updatedDutyDto is null || _dutyRepository is null)
            throw new ArgumentException("Duty name or updated duty or duty Repository cannot be null");
        var updatedDuty = _mapper.Map<Duty>(updatedDutyDto);

        return await _dutyRepository.UpdateDutyInDb(dutyName, updatedDuty);
    }

    public async Task<IEnumerable<DutyDto>> GetDutiesInService()
    {
        if (_dutyRepository is null)
            throw new ArgumentException("Duty Repository cannot be null");
        var duties = await _dutyRepository.GetDutiesFromDb();
        var dutiesDto = _mapper.Map<IEnumerable<DutyDto>>(duties);
        return dutiesDto;
    }

    public async Task<DutyDto> GetDutyByIdInService(int id)
    {
        if (_dutyRepository is null)
            throw new ArgumentException("Duty Repository cannot be null");
        var duty = await _dutyRepository.GetDutyFromDb(id);
        var dutyDto = _mapper.Map<DutyDto>(duty);
        return dutyDto;
    }
}
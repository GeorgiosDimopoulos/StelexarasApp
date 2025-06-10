using AutoMapper;

namespace StelexarasApp.Services.Services;

public class DutyService : IDutyService
{
    private readonly IDutyRepository _dutyRepository = default!;
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

    public async Task<bool> AddDutyInService(CreateDutyRequest dutyDto)
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
    public async Task<bool> DeleteDutyInService(DeleteDutyRequest dutyDto)
    {
        var duty = _mapper.Map<Duty>(dutyDto);
        return await _dutyRepository.DeleteDutyInDb(duty);
    }

    public async Task<bool> UpdateDutyInService(UpdateDutyRequest updatedDutyDto)
    {
        var updatedDuty = _mapper.Map<Duty>(updatedDutyDto);

        return await _dutyRepository.UpdateDutyInDb(updatedDuty);
    }

    public async Task<IEnumerable<DutyResponse>> GetDutiesInService()
    {
        var duties = await _dutyRepository.GetDutiesFromDb();
        var dutiesDto = _mapper.Map<IEnumerable<DutyResponse>>(duties);
        return dutiesDto;
    }

    public async Task<DutyResponse> GetDutyByIdInService(int id)
    {
        var duty = await _dutyRepository.GetDutyFromDb(id);
        var dutyDto = _mapper.Map<DutyResponse>(duty);
        return dutyDto;
    }
}
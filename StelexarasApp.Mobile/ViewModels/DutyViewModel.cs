using System.Collections.ObjectModel;

namespace StelexarasApp.Mobile.ViewModels;

public class DutyViewModel
{
    private readonly IDutyService _dutyService;

    public ObservableCollection<DutyDtoBase> Duties { get; set; } = [];

    // public Command<Duty> DeleteDutyCommand { get; }
    public DutyViewModel(IDutyService dutyService)
    {
        _dutyService = dutyService;

        // DeleteDutyCommand = new Command<Duty>(async (duty) => await DeleteDutyAsync(duty));
        LoadDuties();
    }

    public async Task<bool> DeleteDuty(DeleteDutyRequest deleteDutyRequest)
    {
        var result = await _dutyService.DeleteDutyInService(deleteDutyRequest);

        if (result)
            return true;
        return false;
    }

    public async Task<bool> UpdateDuty(UpdateDutyRequest duty, string dutyNewName)
    {
        if (dutyNewName == null)
            return false;

        duty.Name = dutyNewName;

        var result = await _dutyService.UpdateDutyInService(duty);
        if (result)
            return true;

        return false;
    }

    private void LoadDuties()
    {
        var duties = _dutyService.GetDutiesInService().Result;
        Duties.Clear();

        foreach (var duty in duties)
            Duties.Add(duty);
    }

    public async Task<bool> AddDuty(string dutyName)
    {
        var duty = new CreateDutyRequest
        {
            Name = dutyName
        };
        var result = await _dutyService.AddDutyInService(duty);

        if (result)
        {
            Duties.Add(duty);
            return true;
        }
        return false;
    }
}

using System.Collections.ObjectModel;
using StelexarasApp.Library.Models;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Mobile.ViewModels;

public class DutyViewModel
{
    private readonly IDutyService _dutyService;

    public ObservableCollection<Duty> Duties { get; set; } = [];

    // public Command<Duty> DeleteDutyCommand { get; }
    public DutyViewModel(IDutyService dutyService)
    {
        _dutyService = dutyService;

        // DeleteDutyCommand = new Command<Duty>(async (duty) => await DeleteDutyAsync(duty));
        LoadDuties();
    }

    public async Task<bool> DeleteDuty(int id)
    {
        var result = await _dutyService.DeleteDutyInService(id);

        if (result)
            return true;
        return false;
    }

    public async Task<bool> UpdateDuty(Duty duty, string dutyNewName)
    {
        if (dutyNewName == null)
            return false;

        var result = await _dutyService.UpdateDutyInService(dutyNewName, duty);
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
        var duty = new Duty
        {
            Name = dutyName,
            Date = DateTime.Now
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

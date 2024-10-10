using System.Collections.ObjectModel;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.ViewModels;

public class DutyViewModel
{
    private readonly IDutyService _dutyService;
    public ObservableCollection<Duty> Duties { get; set; } = [];

    public DutyViewModel(IDutyService dutyService)
    {
        _dutyService = dutyService;        
        LoadDuties();
    }

    public async Task<bool> AddDuty(string dutyName)
    {
        if (string.IsNullOrEmpty(dutyName))
            return false;

        var duty = new Duty
        {
            Name = dutyName,
            Date = DateTime.Now
        };

        var result = await _dutyService.AddDutyInService(duty);
        if (result)
            return true;

        return false;
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
}

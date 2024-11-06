using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace StelexarasApp.ViewModels.PeopleViewModels;

public class StaffViewModel : INotifyPropertyChanged
{
    private readonly IStaffService _staffService;
    public ObservableCollection<IStelexosDto> AllStaff { get; set; }

    public StaffViewModel(IStaffService staffService)
    {
        _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
        AllStaff = new ObservableCollection<IStelexosDto>();
        _ = LoadAllStaffAsync();
    }

    public async Task LoadAllStaffAsync()
    {
        try
        {
            var allStaff = await _staffService.GetAllStaffInService();

            if (allStaff == null)
                return;

            AllStaff.Clear();

            foreach (var staff in allStaff)
                AllStaff.Add(staff);

            OnPropertyChanged(nameof(AllStaff));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading staff: {ex.Message}");
        }
    }

    public async Task<IEnumerable<IStelexosDto>> GetAllStaff()
    {
        return await _staffService.GetAllStaffInService();
    }

    public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeKoinotita(KoinotitaDto koinotitaDto)
    {
        return await _staffService.GetOmadarxesSeKoinotitaInService(koinotitaDto);
    }

    public async Task<IEnumerable<KoinotarxisDto>> GetAllKoinotarxes()
    {
        return await _staffService.GetAllKoinotarxesInService();
    }

    public async Task<IEnumerable<OmadarxisDto>> GetAllOmadarxes()
    {
        return await _staffService.GetAllOmadarxesInService();
    }

    public async Task<IEnumerable<TomearxisDto>> GetAllTomearxes()
    {
        return await _staffService.GetAllTomearxesInService();
    }

    public async Task<IEnumerable<OmadarxisDto>> GetOmadarxesSeTomea(TomeasDto tomeasDto)
    {
        return await _staffService.GetOmadarxesSeTomeaInService(tomeasDto);
    }

    public async Task<IEnumerable<KoinotarxisDto>> GetKoinotarxesSeTomea(TomeasDto tomeasDto)
    {
        return await _staffService.GetKoinotarxesSeTomeaInService(tomeasDto);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
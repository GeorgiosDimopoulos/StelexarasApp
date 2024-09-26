using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace StelexarasApp.ViewModels.PeopleViewModels;

public class StaffViewModel : INotifyPropertyChanged
{
    private readonly IStaffService _staffService;
    public ObservableCollection<StelexosDto> AllStaff { get; set; }

    public StaffViewModel(IStaffService staffService)
    {
        _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
        AllStaff = new ObservableCollection<StelexosDto>();
        _ = LoadAllStaffAsync();
    }

    public async Task LoadAllStaffAsync()
    {
        try
        {
            //var tomearxes = await GetAllTomearxes();
            //var koinotarxes = await GetAllKoinotarxes();
            //var omadarxes = await GetAllOmadarxes();
            //var allStaff = tomearxes.Cast<StelexosDto>()
            //    .Concat(koinotarxes.Cast<StelexosDto>())
            //    .Concat(omadarxes.Cast<StelexosDto>());
            var allStaff = await _staffService.GetAllStaffInService();

            AllStaff.Clear();
            foreach (var staff in allStaff)
            {
                AllStaff.Add(staff);
            }

            OnPropertyChanged(nameof(AllStaff));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading staff: {ex.Message}");
        }
    }

    public async Task<IEnumerable<StelexosDto>> GetAllStaff()
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
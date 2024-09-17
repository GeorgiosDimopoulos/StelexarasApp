using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.ViewModels;

public class StaffViewModel : INotifyPropertyChanged
{
    private readonly IStaffService _staffService;
    public ObservableCollection<StelexosDto> AllStaff { get; set; }

    public StaffViewModel(IStaffService staffService, string thesiStr)
    {
        this._staffService = staffService;
        AllStaff = new ObservableCollection<StelexosDto>();
        _ = LoadAllStaffAsync();
    }

    public async Task LoadAllStaffAsync()
    {
        var tomearxes = await GetAllTomearxes();
        var koinotarxes = await GetAllKoinotarxes();
        var omadarxes = await GetAllOmadarxes();

        var allStaff = tomearxes.Cast<StelexosDto>()
            .Concat(koinotarxes.Cast<StelexosDto>())
            .Concat(omadarxes.Cast<StelexosDto>());

        AllStaff = new ObservableCollection<StelexosDto>(allStaff);
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
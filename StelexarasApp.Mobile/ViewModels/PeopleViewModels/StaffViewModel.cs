using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services;
using StelexarasApp.Services.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels;

public class StaffViewModel : INotifyPropertyChanged
{
    private readonly IStaffService _staffService;
    private readonly IApiService _apiService;

    public ObservableCollection<IStelexosDto> AllStaff { get; set; }

    public StaffViewModel(IStaffService staffService, IApiService apiService)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
        AllStaff = new ObservableCollection<IStelexosDto>();
        _ = LoadAllStaffAsync();
    }

    public async Task LoadAllStaffAsync()
    {
        try
        {
            AllStaff.Clear();

            var allStaff = await _staffService.GetAllStaffInService();
            
            if (allStaff == null)
                return;

            AllStaff = allStaff as ObservableCollection<IStelexosDto> ?? new ObservableCollection<IStelexosDto>(allStaff);

            // var allStaff = await _apiService.GetStelexi();
            //foreach (var stelexos in allStaff)
            //{
            //    var stelexosDto = new StelexosDto
            //    {
            //        FullName = stelexos.FullName,
            //        Age = stelexos.Age,
            //        Id = stelexos.Id,
            //        DtoXwrosName = stelexos.XwrosName,
            //        Tel = stelexos.Tel,
            //        Thesi = stelexos.Thesi,
            //    };
            //    AllStaff.Add(stelexosDto);
            //}

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
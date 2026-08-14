using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels;

public class StaffViewModel : INotifyPropertyChanged
{
    private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> _staffService;
    private readonly IApiService _apiService;

    public ObservableCollection<StelexosResponse> AllStaff { get; set; }

    public StaffViewModel(IApiService apiService, IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> staffService)
    {
        _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        AllStaff = new ObservableCollection<StelexosResponse>();
        _ = LoadAllStaffAsync();
    }

    public async Task LoadAllStaffAsync()
    {
        try
        {
            AllStaff.Clear();

            var allStaff = await _staffService.GetStelexi(string.Empty, new()
            {
                IncludeXwros = true
            });

            if (allStaff == null)
                return;

            AllStaff = allStaff as ObservableCollection<StelexosResponse> ?? new ObservableCollection<StelexosResponse>(allStaff);

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

    public async Task<IEnumerable<StelexosResponse>> GetAllStaff()
    {
        return await _staffService.GetStelexi(string.Empty, new());
    }

    public async Task<IEnumerable<StelexosResponse>> GetOmadarxesSeKoinotita(KoinotitaDtoBase koinotitaDto)
    {
        return await _staffService.GetStelexi(koinotitaDto.Name, new());
    }

    public async Task<IEnumerable<StelexosResponse>> GetAllKoinotarxes()
    {
        return await _staffService.GetStelexi(string.Empty, new());
    }

    public async Task<IEnumerable<StelexosResponse>> GetAllOmadarxes()
    {
        return await _staffService.GetStelexi(string.Empty, new());
    }

    public async Task<IEnumerable<StelexosResponse>> GetAllTomearxes()
    {
        return await _staffService.GetStelexi(string.Empty, new());
    }

    public async Task<IEnumerable<StelexosResponse>> GetOmadarxesSeTomea(TomeasDtoBase tomeasDto)
    {
        return await _staffService.GetStelexi(tomeasDto.Name, new());

    }

    public async Task<IEnumerable<StelexosResponse>> GetKoinotarxesSeTomea(TomeasDtoBase tomeasDto)
    {
        return await _staffService.GetStelexi(tomeasDto.Name, new());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
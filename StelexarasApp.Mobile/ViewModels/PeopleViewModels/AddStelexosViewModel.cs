using System.Collections.ObjectModel;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels;

public class AddStelexosViewModel
{
    private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> _staffService;
    private readonly ITeamsService _teamsService;

    public ObservableCollection<string> ThesiOptions { get; set; }
    public string SelectedThesi { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string XwrosName { get; set; } = string.Empty;
    public int Age { get; set; } = default!;
    public Sex Sex { get; set; } = default!;
    public Command SaveCommand { get; }

    public AddStelexosViewModel(IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> staffService, ITeamsService teamsService)
    {
        _staffService = staffService;
        _teamsService = teamsService;
        ThesiOptions = [.. Enum.GetNames(typeof(Thesi))];
        SaveCommand = new Command(OnSaveStelexos);
    }

    public async Task<bool> TrySaveStelexosAsync()
    {
        if (!IsValidFullNameInput(FullName) || string.IsNullOrWhiteSpace(XwrosName) || string.IsNullOrWhiteSpace(PhoneNumber) || Age < 18)
            return false;

        switch (Enum.Parse<Thesi>(SelectedThesi))
        {
            case Thesi.None:
                await Application.Current.MainPage.DisplayAlert("ΣΦΆΛΜΑ", "Παρακαλώ επιλέξτε θέση", "OK");
                return false;
            case Thesi.Omadarxis:
                var omadarxis = new CreateStelexosRequest
                {
                    FullName = FullName,
                    XwrosName = XwrosName,
                    Tel = PhoneNumber,
                    Age = Age,
                    Thesi = Thesi.Omadarxis,
                    Sex = Sex
                };
                await _staffService.CreateStelexos(omadarxis, Thesi.Omadarxis);
                break;
            case Thesi.Koinotarxis:
                var koinotarxis = new CreateStelexosRequest
                {
                    FullName = FullName,
                    Thesi = Thesi.Koinotarxis,
                    XwrosName = XwrosName,
                    Tel = PhoneNumber,
                    Age = Age,
                    Sex = Sex
                };
                await _staffService.CreateStelexos(koinotarxis, Thesi.Koinotarxis);
                break;
            case Thesi.Tomearxis:
                var tomearxis = new CreateStelexosRequest
                {
                    FullName = FullName,
                    Thesi = Thesi.Tomearxis,
                    XwrosName = XwrosName,
                    Tel = PhoneNumber,
                    Age = Age,
                    Sex = Sex
                };
                await _staffService.CreateStelexos(tomearxis, Thesi.Tomearxis);
                break;
        }

        return true;
    }

    private async void OnSaveStelexos(object obj)
    {
        var isSuccess = await TrySaveStelexosAsync();
        if (!isSuccess)
            await Application.Current.MainPage.DisplayAlert("ΣΦΆΛΜΑ", "Παρακαλώ σημειώστε σωστά όλα τα πεδία", "OK");
    }

    private static bool IsValidFullNameInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var parts = input.Trim().Split(' ');
        return parts.Length >= 2;
    }
}
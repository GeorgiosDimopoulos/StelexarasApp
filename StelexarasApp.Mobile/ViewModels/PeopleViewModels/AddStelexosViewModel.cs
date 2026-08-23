using System.Collections.ObjectModel;

namespace StelexarasApp.Mobile.ViewModels.PeopleViewModels;

public class AddStelexosViewModel
{
    private readonly IStaffService _staffService;
    private readonly ITeamsService _teamsService;

    public ObservableCollection<string> ThesiOptions { get; set; }
    public string SelectedThesi { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string XwrosName { get; set; } = string.Empty;
    public int Age { get; set; } = default!;
    public Sex Sex { get; set; } = default!;
    public Command SaveCommand { get; }

    public AddStelexosViewModel(IStaffService staffService, ITeamsService teamsService)
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
                    FirstName = FullName.Split(' ')[0],
                    LastName = string.Join(' ', FullName.Split(' ')[1..]),
                    XwrosName = XwrosName,
                    Tel = PhoneNumber,
                    Age = Age,
                    Thesi = Thesi.Omadarxis,
                    Sex = Sex
                };
                await _staffService.CreateStelexos(omadarxis);
                break;
            case Thesi.Koinotarxis:
                var koinotarxis = new CreateStelexosRequest
                {
                    FirstName = FullName.Split(' ')[0],
                    LastName = string.Join(' ', FullName.Split(' ')[1..]),
                    Thesi = Thesi.Koinotarxis,
                    XwrosName = XwrosName,
                    Tel = PhoneNumber,
                    Age = Age,
                    Sex = Sex
                };
                await _staffService.CreateStelexos(koinotarxis);
                break;
            case Thesi.Tomearxis:
                var tomearxis = new CreateStelexosRequest
                {
                    FirstName = FullName.Split(' ')[0],
                    LastName = string.Join(' ', FullName.Split(' ')[1..]),
                    Thesi = Thesi.Tomearxis,
                    XwrosName = XwrosName,
                    Tel = PhoneNumber,
                    Age = Age,
                    Sex = Sex
                };
                await _staffService.CreateStelexos(tomearxis);
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
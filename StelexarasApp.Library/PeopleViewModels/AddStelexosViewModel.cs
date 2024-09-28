using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services.IServices;
using System.Collections.ObjectModel;

namespace StelexarasApp.ViewModels.PeopleViewModels;

public class AddStelexosViewModel
{
    private readonly IStaffService _staffService;
    private readonly ITeamsService _teamsService;

    public ObservableCollection<string> ThesiOptions { get; set; }
    public string SelectedThesi { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string XwrosName { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public Command SaveCommand { get; }

    /add unit tests
    public AddStelexosViewModel(IStaffService staffService, ITeamsService teamsService)
    {
        _staffService = staffService;
        _teamsService = teamsService;
        ThesiOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Thesi)));
        SaveCommand = new Command(OnSaveStelexos);
    }

    private async void OnSaveStelexos(object obj)
    {
        if (IsValidFullNameInput(FullName) || string.IsNullOrWhiteSpace(XwrosName) || string.IsNullOrWhiteSpace(PhoneNumber) || Age < 18)
        {
            await Application.Current.MainPage.DisplayAlert("ΣΦΆΛΜΑ", "Παρακαλώ σημειώστε σωστά όλα τα πεδία", "OK");
            return;
        }

        var newStelexosDto = new StelexosDto
        {
            FullName = FullName,
            XwrosName = XwrosName,
            Tel = PhoneNumber,
            Age = Age,
            Thesi = Enum.Parse<Thesi>(SelectedThesi)
        };

        bool isOkToBeAdded = await _teamsService.CheckStelexousXwroNameInService(newStelexosDto, XwrosName);

        await _staffService.AddStelexosInService(newStelexosDto);
    }

    public static bool IsValidFullNameInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var parts = input.Trim().Split(' ');
        return parts.Length >= 2;
    }
}

﻿using StelexarasApp.DataAccess.Models.Atoma.Staff;
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

    public AddStelexosViewModel(IStaffService staffService, ITeamsService teamsService)
    {
        _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
        _teamsService = teamsService ?? throw new ArgumentNullException(nameof(teamsService));
        ThesiOptions = new ObservableCollection<string>(Enum.GetNames(typeof(Thesi)));
        SaveCommand = new Command(OnSaveStelexos);
    }

    public async Task<bool> TrySaveStelexosAsync()
    {
        if (!IsValidFullNameInput(FullName) || string.IsNullOrWhiteSpace(XwrosName) || string.IsNullOrWhiteSpace(PhoneNumber) || Age < 18)
            return false;

        var newStelexosDto = new StelexosDto
        {
            FullName = FullName,
            XwrosName = XwrosName,
            Tel = PhoneNumber,
            Age = Age,
            Thesi = Enum.Parse<Thesi>(SelectedThesi)
        };

        bool isOkToBeAdded = await _teamsService.CheckStelexousXwroNameInService(newStelexosDto, XwrosName);
        if (!isOkToBeAdded)
            return false;

        await _staffService.AddStelexosInService(newStelexosDto);
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
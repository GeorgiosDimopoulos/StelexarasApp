using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.PeopleViewModels;

namespace StelexarasApp.Views.StaffViews;

public partial class AddStelexosPage : ContentPage
{
	private readonly IStaffService _staffService;
    private readonly ITeamsService _teamsService;
    private readonly AddStelexosViewModel _addStelexosViewModel;
    
    public AddStelexosPage(IStaffService staffService, ITeamsService teamsService)
	{
		InitializeComponent();
        _staffService = staffService;
        _teamsService = teamsService;
        _addStelexosViewModel = new AddStelexosViewModel(_staffService, _teamsService);
    }
}
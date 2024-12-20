using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Mobile.ViewModels.PeopleViewModels;

namespace StelexarasApp.Mobile.Views.StaffViews;

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
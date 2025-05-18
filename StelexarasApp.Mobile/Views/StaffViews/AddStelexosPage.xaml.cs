namespace StelexarasApp.Mobile.Views.StaffViews;

public partial class AddStelexosPage : ContentPage
{
	private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse> _staffService;
    private readonly ITeamsService _teamsService;
    private readonly AddStelexosViewModel _addStelexosViewModel;
    
    public AddStelexosPage(IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse> staffService, ITeamsService teamsService)
	{
		InitializeComponent();
        _staffService = staffService;
        _teamsService = teamsService;
        _addStelexosViewModel = new AddStelexosViewModel(_staffService, _teamsService);
    }
}
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.PeopleViewModels;

namespace StelexarasApp.Views.StaffViews;

public partial class AddStelexosPage : ContentPage
{
	private readonly IStaffService _staffService;
    // private readonly AddStelexosViewModel _addStelexosViewModel;
    
    public AddStelexosPage(IStaffService staffService)
	{
		InitializeComponent();
        _staffService = staffService;
        // _addStelexosViewModel = new AddStelexosViewModel(_staffService);
    }
}
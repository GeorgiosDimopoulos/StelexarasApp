using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.PeopleViewModels;
using StelexarasApp.Views.StaffViews;

namespace StelexarasApp.UI.Views.StaffViews;

public partial class StaffPage : ContentPage
{
    private readonly IStaffService _personalService;
    private readonly ITeamsService _teamsService;
    private readonly StaffViewModel _personalViewModel;

    public StaffPage(IStaffService personalService, ITeamsService teamsService, StaffViewModel personalViewModel)
    {
        _personalService = personalService;
        _teamsService = teamsService;
        InitializeComponent();
        _personalViewModel = personalViewModel;
        BindingContext = _personalViewModel;
    }

    private async void OnStelexosSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var staffService = DependencyService.Get<IStaffService>();
            StelexosDto? selectedStaff = e.CurrentSelection [0] as StelexosDto;
            if (selectedStaff != null)
            {
                var stelexosInfoPage = new StelexosInfoPage(staffService, selectedStaff, null);
                await Navigation.PushAsync(stelexosInfoPage);
            }
            else
            {
                throw new ArgumentNullException(nameof(selectedStaff), "Selected staff cannot be null.");
            }
        }
    }

    public string GetThesiValue(Thesi thesi)
    {
        return thesi switch
        {
            Thesi.Tomearxis => "Τομεάρχες",
            Thesi.Ekpaideutis => "Εκπαιδευτές",
            Thesi.Omadarxis => "Ομαδάρχες",
            Thesi.Koinotarxis => "Κοινοτάρχες",
            _ => "Unknown Thesi Title"
        };
    }

    private async void OnAddStelexosClicked(object sender, EventArgs e)
    {
        var addStelexosPage = new AddStelexosPage(_personalService, _teamsService);
        await Navigation.PushAsync(addStelexosPage);
    }
}
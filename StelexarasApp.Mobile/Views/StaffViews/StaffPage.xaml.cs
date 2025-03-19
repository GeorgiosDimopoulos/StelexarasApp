using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Mobile.ViewModels.PeopleViewModels;

namespace StelexarasApp.Mobile.Views.StaffViews;

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
            int stelexosId = (e.CurrentSelection [0] as IStelexos).Id;
            var selectedWorkerDto = e.CurrentSelection [0] as IStelexosDto;
            if (selectedWorkerDto != null)
            {
                var stelexosInfoPage = new StelexosInfoPage(staffService, selectedWorkerDto, stelexosId);
                await Navigation.PushAsync(stelexosInfoPage);
            }
            else
            {
                throw new ArgumentNullException(nameof(selectedWorkerDto), "Selected staff cannot be null.");
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
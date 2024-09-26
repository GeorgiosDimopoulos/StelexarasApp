using StelexarasApp.Services;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.UI.Views.PaidiaViews;
using StelexarasApp.UI.Views.StaffViews;
using StelexarasApp.Views.TeamsViews;

namespace StelexarasApp.UI.Views;

public partial class MainPage : ContentPage
{
    private readonly IExpenseService _expenseService;
    private readonly IStaffService _stelexiService;
    private readonly IPaidiaService _paidiaService;
    private readonly IDutyService _dutiesService;
    private readonly ITeamsService _teamsService;
    private readonly IStaffService _staffService;
    private readonly SignalrService _signalRService;
    private readonly IServiceProvider _serviceProvider;

    public MainPage(
        IStaffService peopleService,
        IDutyService dutyService,
        IPaidiaService paidiaService,
        ITeamsService teamsService,
        IExpenseService expenseService,
        IStaffService staffService,
        SignalrService signalRService,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _dutiesService = dutyService;
        _stelexiService = peopleService;
        _paidiaService = paidiaService;
        _teamsService = teamsService;
        _expenseService = expenseService;
        _staffService = staffService;
        _signalRService = signalRService;
        _serviceProvider = serviceProvider;
    }

    private void OnMessageReceived(string message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            DisplayAlert("Message Received", message, "OK");
        });
    }

    private async void SendMessageButton_Clicked(object sender, EventArgs e)
    {
        await _signalRService.SendMessageAsync("Hello from MAUI!");
    }

    private async void OnExpensesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ExpensesPage(_expenseService));
    private async void OnTeamsPageButtonClicked(object sender, EventArgs e)
    {
        //if (_teamsService is null)
        //{
        //    await DisplayAlert("Προσοχή", "Η υπηρεσία με τους τομείς δεν είναι διαθέσιμη", "OK");
        //    return;
        //}
        await Navigation.PushAsync(new GeneralTeamsPage(_paidiaService, _teamsService));
    }

    private async void OnStaffButtonClicked(object sender, EventArgs e)
    {
        var staffPage = ActivatorUtilities.CreateInstance<StaffPage>(_serviceProvider, _stelexiService);
        await Navigation.PushAsync(staffPage);
    }
    private async void OnDutiesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ToDoPage(_dutiesService));
    private async void OnPaidiaButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PaidiaPage(_paidiaService));
}
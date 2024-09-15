using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.UI.Views
{
    public partial class MainPage : ContentPage
    {
        private IExpenseService _expenseService;
        private IStaffService _stelexiService;
        private IPaidiaService _paidiaService;
        private IDutyService _dutiesService;
        private ITeamsService _teamsService;

        public MainPage(
            IStaffService peopleService,
            IDutyService dutyService,
            IPaidiaService paidiaService,
            ITeamsService teamsService,
            IExpenseService expenseService)
        {
            InitializeComponent();
            _dutiesService = dutyService;
            _stelexiService = peopleService;
            _paidiaService = paidiaService;
            _expenseService = expenseService;
            _teamsService = teamsService;
        }

        private async void OnExpensesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ExpensesPage(_expenseService));
        private async void OnPeopleButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new TeamsPage(_paidiaService, _teamsService, EidosXwrou.Skini));
        private async void OnPersonalButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new StaffPage(_stelexiService, Thesi.Tomearxis));
        private async void OnDutiesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ToDoPage(_dutiesService));
    }
}
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.UI.Views
{
    public partial class MainPage : ContentPage
    {
        private IExpenseService _expenseService;
        private IStelexiService _stelexiService;
        private IPaidiaService _paidiaService;
        private IDutyService _dutiesService;
        private ITeamsService _teamsService;

        public MainPage(
            IStelexiService peopleService,
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
        private async void OnPeopleButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new TeamsPage(_paidiaService, EidosXwrou.Skini));
        private async void OnPersonalButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new StuffPage(_stelexiService, Thesi.Omadarxis));
        private async void OnDutiesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ToDoPage(_dutiesService));
    }
}
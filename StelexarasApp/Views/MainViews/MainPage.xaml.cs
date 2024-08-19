using StelexarasApp.Services.IServices;

namespace StelexarasApp.UI.Views
{
    public partial class MainPage : ContentPage
    {
        private IExpenseService _expenseService;
        private ITeamsService _teamsService;
        private IPersonalService _personalService;
        private IDutyService _dutiesService;

        public MainPage(
            ITeamsService peopleService,
            IDutyService dutyService,
            IPersonalService personalService,
            IExpenseService expenseService)
        {
            InitializeComponent();
            _dutiesService = dutyService;
            _teamsService = peopleService;
            _personalService = personalService;
            _expenseService = expenseService;
        }

        private async void OnExpensesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ExpensesPage(_expenseService));
        private async void OnPeopleButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new TeamsPage(_teamsService));
        private async void OnPersonalButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PersonalPage(_personalService));

        private async void OnDutiesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ToDoPage(_dutiesService));
    }
}
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;

namespace StelexarasApp.UI.Views
{
    public partial class MainPage : ContentPage
    {
        private IExpenseService _expenseService;
        private IPeopleService _peopleService;
        private IPersonalService _personalService;
        private IDutyService _dutiesService;

        public MainPage(
            IPeopleService peopleService,
            IDutyService dutyService,
            IPersonalService personalService,
            IExpenseService expenseService)
        {
            InitializeComponent();
            _dutiesService = dutyService;
            _peopleService = peopleService;
            _personalService = personalService;
            _expenseService = expenseService;
        }

        private async void OnExpensesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ExpensesPage(_expenseService));
        private async void OnPeopleButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PeoplePage(_peopleService));
        private async void OnPersonalButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PersonalPage(_personalService));

        private async void OnDutiesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ToDoPage(_dutiesService));
    }
}
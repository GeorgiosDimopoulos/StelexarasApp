using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;

namespace StelexarasApp.UI.Views
{
    public partial class MainPage : ContentPage
    {
        private IExpenseService _expenseService;
        private IPeopleService _peopleService;

        public MainPage(IPeopleService peopleService, IExpenseService expenseService)
        {
            InitializeComponent();
            _peopleService = peopleService;
            _expenseService = expenseService;
        }

        private async void OnExpensesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ExpensesPage(_expenseService));
        private async void OnPeopleButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new KoinotitaPage(_peopleService));
        private async void OnPersonalButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ProswpikaPage());

        private async void OnDutiesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ToDoPage());
    }
}
namespace StelexarasApp.UI.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnExpensesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ExpensesPage());
        private async void OnPeopleButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KoinotitaPage());
        }
        private async void OnPersonalButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ProswpikaPage());

        private async void OnDutiesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ToDoPage());
    }
}

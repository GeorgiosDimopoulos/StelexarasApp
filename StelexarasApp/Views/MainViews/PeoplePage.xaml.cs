using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.Services.IServices;
using StelexarasApp.UI.Views.SecondViews;
using StelexarasApp.ViewModels;

namespace StelexarasApp.UI.Views
{
    public partial class PeoplePage : ContentPage
    {
        private PeopleViewModel _viewModel; 
        private IPeopleService _peopleService;

        public PeoplePage(IPeopleService peopleService)
        {
            InitializeComponent();
            _peopleService = peopleService;
            _viewModel = new PeopleViewModel(peopleService);
            BindingContext = _viewModel;
        }

        private async void Paidi_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            Ekpaideuomenos? paidi = button?.CommandParameter as Ekpaideuomenos;

            if (paidi != null)
            {
                await Navigation.PushAsync(new ChildInfoPage(_peopleService, paidi, _viewModel.Skines));
            }
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            string fullName = await DisplayPromptAsync("Νέο παιδί", "Γράψτε όνομα και επίθετο:", "OK", "Άκυρο", "Ονοματεπώνυμο", maxLength: 50, keyboard: Keyboard.Text);
            string skiniName = await DisplayPromptAsync("Νέο παιδί", "Γράψτε όνομα σκηνής:", "OK", "Άκυρο", "Σκηνή", maxLength: 50, keyboard: Keyboard.Text);

            if (string.IsNullOrEmpty(fullName) || !IsValidInput(fullName, skiniName) || (string.IsNullOrEmpty(skiniName)))
            {
                await DisplayAlert("Λάθος Στοιχεία", $"{fullName}", "OK");
            }
            else 
            {
                if(await _viewModel.AddPaidiAsync(fullName, skiniName) == 1)
                {
                    await DisplayAlert("Στοιχεία νέου παιδιού", fullName, "OK");
                }
            }
        }

        private bool IsValidInput(string fullName, string skiniName)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(skiniName))
            {
                return false;
            }

            var parts = fullName.Trim().Split(' ');
            return parts.Length >= 2;
        }
    }
}

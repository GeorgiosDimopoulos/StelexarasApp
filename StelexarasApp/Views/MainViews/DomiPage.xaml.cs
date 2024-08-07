using StelexarasApp.Library.Models.Atoma.Paidia;
using StelexarasApp.Library.Models.Domi;
using StelexarasApp.Services.ViewModels;

namespace StelexarasApp.Presentation.Views
{
    public partial class DomiPage : ContentPage
    {
        private DomiViewModel _viewModel;

        public DomiPage()
        {
            InitializeComponent();
            
            // BindingContext = new DomiViewModel();
            _viewModel = new DomiViewModel();
            BindingContext = _viewModel;
        }

        private async void Paidi_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            Ekpaideuomenos? paidi = button?.CommandParameter as Ekpaideuomenos;

            if (paidi != null)
            {
                // DisplayAlert("Στοιχεία παιδιού", $"Όνομα: {paidi.FullName}", "OK");
                await Navigation.PushAsync(new ChildInfoPage(paidi, _viewModel.Skines));
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
                if(_viewModel.AddNewEkpaideuomenos(fullName, skiniName) == 1)
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

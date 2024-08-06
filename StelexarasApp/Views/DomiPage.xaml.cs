using StelexarasApp.Library.Models.Atoma.Paidia;
using StelexarasApp.Services.ViewModels;

namespace StelexarasApp.Presentation.Views
{
    public partial class DomiPage : ContentPage
    {
        public DomiPage()
        {
            InitializeComponent();
            BindingContext = new DomiViewModel();
        }

        private void Paidi_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            Ekpaideuomenos? paidi = button?.CommandParameter as Ekpaideuomenos;

            if (paidi != null)
            {
                DisplayAlert("Στοιχεία παιδιού", $"Όνομα: {paidi.FullName}", "OK");
            }
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            DisplayAlert("Στοιχεία παιδιού", "Όνομα:", "OK");
        }
    }
}

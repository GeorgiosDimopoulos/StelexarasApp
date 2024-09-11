using StelexarasApp.Services.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.UI.Views.SecondViews;
using StelexarasApp.DataAccess.Helpers;

namespace StelexarasApp.UI.Views
{
    public partial class TeamsPage : ContentPage
    {
        private TeamsViewModel _viewModel;
        private IPaidiaService _paidiaService;

        public TeamsPage(IPaidiaService paidiaService, EidosXwrou eidosXwrou)
        {
            InitializeComponent();
            _paidiaService = paidiaService;
            _viewModel = new TeamsViewModel(_paidiaService, eidosXwrou);
            BindingContext = _viewModel;
        }

        private async void Paidi_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            PaidiDto? paidi = button?.CommandParameter as PaidiDto;

            if (paidi != null)
            {
                var paidiPage = new PaidiInfoPage(_paidiaService, paidi, _viewModel.Skines);
                await Navigation.PushAsync(paidiPage);
            }
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            string fullName = await DisplayPromptAsync("Νέο παιδί", "Γράψτε όνομα και επίθετο:", "OK", "Άκυρο", "Ονοματεπώνυμο", maxLength: 50, keyboard: Keyboard.Text);
            string skiniName = await DisplayPromptAsync("Νέο παιδί", "Γράψτε όνομα σκηνής:", "OK", "Άκυρο", "Σκηνή", maxLength: 50, keyboard: Keyboard.Text);

            if (string.IsNullOrEmpty(fullName) ||
                !WordsConverterChecks.IsValidFullNameInput(fullName) ||
                !WordsConverterChecks.IsValidFullNameInput(skiniName) ||
                (string.IsNullOrEmpty(skiniName)))
                await DisplayAlert("Λάθος Στοιχεία", $"{fullName}", "OK");
            else
            {
                if (await _viewModel.AddPaidiAsync(fullName, skiniName, PaidiType.Kataskinotis))
                {
                    await DisplayAlert("Στοιχεία νέου παιδιού", fullName, "OK");
                }
            }
        }
    }
}

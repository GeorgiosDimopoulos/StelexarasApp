using StelexarasApp.ViewModels;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;

namespace StelexarasApp.UI.Views.SecondViews
{
    public partial class StelexosInfoPage : ContentPage
    {
        private StelexiInfoViewModel? _stelexosinfoViewModel;
        private StelexosDto stelexosDto1;

        public StelexosInfoPage(IStelexiService stelexiService, StelexosDto stelexosDto)
        {
            InitializeComponent();
            _stelexosinfoViewModel = new StelexiInfoViewModel(stelexosDto, stelexiService);
            stelexosDto1 = stelexosDto;
            BindingContext = _stelexosinfoViewModel; // stelexosDto
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (_stelexosinfoViewModel is null)
            {
                return;
            }

            await _stelexosinfoViewModel.DeleteStelexos(stelexosDto1, Thesi.Omadarxis);
            await Navigation.PopAsync();
        }
    }
}
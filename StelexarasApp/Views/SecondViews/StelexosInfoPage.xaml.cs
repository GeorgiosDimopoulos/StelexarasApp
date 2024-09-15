using StelexarasApp.ViewModels;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.UI.Views.SecondViews
{
    public partial class StelexosInfoPage : ContentPage
    {
        private StelexosInfoViewModel? _stelexosinfoViewModel;
        private StelexosDto stelexosDto1;

        public StelexosInfoPage(IStaffService stelexiService, StelexosDto stelexosDto)
        {
            InitializeComponent();
            _stelexosinfoViewModel = new StelexosInfoViewModel(stelexosDto, stelexiService);
            stelexosDto1 = stelexosDto;
            BindingContext = _stelexosinfoViewModel;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (_stelexosinfoViewModel is null)
            {
                return;
            }

            await _stelexosinfoViewModel.DeleteStelexos(stelexosDto1);
            await Navigation.PopAsync();
        }
    }
}
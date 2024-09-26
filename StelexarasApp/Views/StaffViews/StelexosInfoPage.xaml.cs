using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.PeopleViewModels;

namespace StelexarasApp.UI.Views.StaffViews
{
    public partial class StelexosInfoPage : ContentPage
    {
        private StelexosInfoViewModel? _stelexosinfoViewModel;
        private StelexosDto stelexosDto1;

        public StelexosInfoPage(IStaffService stelexiService, StelexosDto stelexosDto, StelexosInfoViewModel stelexosInfoViewModel)
        {
            InitializeComponent();
            _stelexosinfoViewModel = stelexosInfoViewModel ?? new StelexosInfoViewModel(stelexosDto, stelexiService);
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
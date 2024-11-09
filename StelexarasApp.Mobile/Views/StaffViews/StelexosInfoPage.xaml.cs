using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.PeopleViewModels;

namespace StelexarasApp.Mobile.Views.StaffViews;

public partial class StelexosInfoPage : ContentPage
{
    private readonly StelexosInfoViewModel? _stelexosinfoViewModel;
    private readonly IStelexosDto stelexosDto1;

    public StelexosInfoPage(IStaffService stelexiService, IStelexosDto stelexosDto)
    {
        InitializeComponent();
        if (stelexosDto == null)
            throw new ArgumentNullException(nameof(stelexosDto), "StelexosDto cannot be null");

        _stelexosinfoViewModel = new StelexosInfoViewModel(stelexosDto, stelexiService);
        stelexosDto1 = stelexosDto;
        BindingContext = _stelexosinfoViewModel;
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_stelexosinfoViewModel is null)
            return;

        await _stelexosinfoViewModel.DeleteStelexos(stelexosDto1);
        await Navigation.PopAsync();
    }
}
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Mobile.ViewModels.PeopleViewModels;
using StelexarasApp.Library.Dtos.Atoma;

namespace StelexarasApp.Mobile.Views.StaffViews;

public partial class StelexosInfoPage : ContentPage
{
    private readonly StelexosInfoViewModel? _stelexosinfoViewModel;

    public StelexosInfoPage(IStaffService stelexiService, IStelexosDto stelexos, int id)
    {
        InitializeComponent();

        _stelexosinfoViewModel = new StelexosInfoViewModel(stelexos, id, stelexiService);
        BindingContext = _stelexosinfoViewModel;
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_stelexosinfoViewModel is null)
            return;

        await _stelexosinfoViewModel.DeleteStelexos();
        await Navigation.PopAsync();
    }
}
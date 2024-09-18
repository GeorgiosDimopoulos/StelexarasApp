using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;
using StelexarasApp.UI.Views.TeamsViews;
using StelexarasApp.ViewModels.TeamsViewModels;

namespace StelexarasApp.Views.TeamsViews;

public partial class KoinotitaInfoPage : ContentPage
{
    private IPaidiaService _paidiaService;
    private ITeamsService _teamsService;
    private KoinotitaViewModel _koinotitaViewModel;

    public KoinotitaInfoPage(ITeamsService teamsService, IPaidiaService paidiaService, KoinotitaViewModel koinotitaViewModel)
    {
        InitializeComponent();
        _teamsService = teamsService;
        _koinotitaViewModel = koinotitaViewModel;
        _paidiaService = paidiaService;
    }

    private async void SkiniButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var skini = button?.CommandParameter as SkiniDto;

        if (skini != null)
        {
            var skiniPage = new SkiniInfoPage(skini, _paidiaService);
            await Navigation.PushModalAsync(skiniPage);
        }
    }
}
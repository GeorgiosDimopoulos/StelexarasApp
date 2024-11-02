using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.TeamsViewModels;

namespace StelexarasApp.UI.Views.TeamsViews;

public partial class KoinotitaInfoPage : ContentPage
{
    private IPaidiaService _paidiaService;
    private ITeamsService _teamsService;
    private Koinotita MyKoinotita;

    public KoinotitaDto Koinotita { get; set; }

    public KoinotitaInfoPage(ITeamsService teamsService, IPaidiaService paidiaService, KoinotitaViewModel koinotitaViewModel)
    {
        InitializeComponent();
        _teamsService = teamsService;
        _paidiaService = paidiaService;
        MyKoinotita = koinotitaViewModel.Koinotita;
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
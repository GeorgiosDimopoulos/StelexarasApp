using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;
using StelexarasApp.UI.Views.TeamsViews;
using StelexarasApp.ViewModels.TeamsViewModels;

namespace StelexarasApp.Views.TeamsViews;

public partial class GeneralTeamsPage : ContentPage
{
    private readonly TomeasViewModel _tomeas2ViewModel;
    private readonly TomeasViewModel _tomeas1ViewModel;
    private readonly KoinotitaViewModel _koinotitaViewModel;
    private readonly IPaidiaService _paidiaService;
    private readonly ITeamsService _teamsService;

    public GeneralTeamsPage(TomeasDto tomeas1, TomeasDto tomeas2, IPaidiaService paidiaService, ITeamsService teamsService)
    {
        InitializeComponent();
        _paidiaService = paidiaService;
        _teamsService = teamsService;
        _tomeas1ViewModel = new TomeasViewModel(tomeas1, _teamsService, _paidiaService);
        _tomeas2ViewModel = new TomeasViewModel(tomeas2, _teamsService, _paidiaService);
        _koinotitaViewModel = new KoinotitaViewModel(_paidiaService, _teamsService);
    }

    private async void TomeasA_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TomeasPage(_tomeas1ViewModel, _koinotitaViewModel, _teamsService, _paidiaService));
    }

    private async void TomeasB_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TomeasPage(_tomeas2ViewModel, _koinotitaViewModel, _teamsService, _paidiaService));
    }

    private async void Sxoli_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KoinotitaPage(_paidiaService, _teamsService, _koinotitaViewModel));
    }
}
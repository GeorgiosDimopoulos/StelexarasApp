using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels.TeamsViewModels;

namespace StelexarasApp.Views.TeamsViews;

public partial class TomeasInfoPage : ContentPage
{
    private TomeasViewModel _tomeasViewModel;
    private KoinotitaViewModel _koinotitaViewModel;
    private IPaidiaService _paidiaService;
    private ITeamsService _teamsService;

    public TomeasInfoPage(TomeasViewModel tomeasViewModel, KoinotitaViewModel koinotitaViewModel, ITeamsService teamsService, IPaidiaService paidiaService)
    {
        InitializeComponent();
        _teamsService = teamsService;
        _paidiaService = paidiaService;
        _tomeasViewModel = tomeasViewModel;
        _koinotitaViewModel = koinotitaViewModel;
        BindingContext = _tomeasViewModel;
    }

    private void KoinotitaButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var koinotita = (Koinotita)button.BindingContext;
        _koinotitaViewModel.Koinotita = koinotita;

        Navigation.PushAsync(new KoinotitaInfoPage(_teamsService, _paidiaService, _koinotitaViewModel));
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        var newKoinotita = new KoinotitaDto
        {
            TomeasName = _tomeasViewModel.TomeasDto.Name,
            Name = "",
        };
        _koinotitaViewModel.AddKoinotita(newKoinotita);
    }
}
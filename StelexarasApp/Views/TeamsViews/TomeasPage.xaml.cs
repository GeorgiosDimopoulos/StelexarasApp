using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.IServices;
using StelexarasApp.UI.Views.TeamsViews;
using StelexarasApp.ViewModels.TeamsViewModels;

namespace StelexarasApp.Views.TeamsViews;

public partial class TomeasPage : ContentPage
{
    private TomeasViewModel _teamsViewModel;
    private KoinotitaViewModel _koinotitaViewModel;
    private IPaidiaService _paidiaService;
    private ITeamsService _teamsService;

    public TomeasPage(TomeasViewModel teamsViewModel, KoinotitaViewModel koinotitaViewModel, ITeamsService teamsService, IPaidiaService paidiaService)
    {
        InitializeComponent();
        _teamsService = teamsService;
        _paidiaService = paidiaService;
        _teamsViewModel = teamsViewModel;
        _koinotitaViewModel = koinotitaViewModel;
        BindingContext = _teamsViewModel;
    }

    private void KoinotitaButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var koinotita = (Koinotita)button.BindingContext;
        _koinotitaViewModel.Koinotita = koinotita;

        Navigation.PushAsync(new KoinotitaPage(_paidiaService, _teamsService, _koinotitaViewModel));
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        var newKoinotita = new KoinotitaDto 
        {
            TomeasName = _teamsViewModel.TomeasDto.Name,
            Name = "",
        };
        _koinotitaViewModel.AddKoinotita(newKoinotita);
    }
}
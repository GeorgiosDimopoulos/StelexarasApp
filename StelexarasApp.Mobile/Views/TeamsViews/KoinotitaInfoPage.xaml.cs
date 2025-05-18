namespace StelexarasApp.Mobile.Views.TeamsViews;

public partial class KoinotitaInfoPage : ContentPage
{
    private IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiaService;
    private ITeamsService _teamsService;
    public KoinotitaResponse Koinotita { get; set; }

    public KoinotitaInfoPage(ITeamsService teamsService, IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> paidiaService, KoinotitaViewModel koinotitaViewModel)
    {
        InitializeComponent();
        _teamsService = teamsService;
        _paidiaService = paidiaService;
        Koinotita = koinotitaViewModel.Koinotita ?? new KoinotitaResponse();
    }

    private async void SkiniButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var skini = button?.CommandParameter as SkiniResponse;

        if (skini != null)
        {
            var skiniPage = new SkiniInfoPage(skini, _paidiaService);
            await Navigation.PushModalAsync(skiniPage);
        }
    }
}
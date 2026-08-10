using StelexarasApp.Mobile.Factories;

namespace StelexarasApp.Mobile.Views.TeamsViews;

public partial class KoinotitaInfoPage : ContentPage
{
    private IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> _paidiaService;
    private ITeamsService _teamsService;
    private IPageFactory _pageFactory;

    public KoinotitaResponse Koinotita { get; set; }

    public KoinotitaInfoPage(ITeamsService teamsService,
        IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> paidiaService,
        KoinotitaViewModel koinotitaViewModel,
        IPageFactory pageFactory)
    {
        InitializeComponent();
        _teamsService = teamsService;

        _paidiaService = paidiaService;
        _pageFactory = pageFactory;
        Koinotita = koinotitaViewModel.Koinotita ?? new KoinotitaResponse();
    }

    private async void SkiniButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var skini = button?.CommandParameter as SkiniResponse;

        if (skini != null)
        {
            var skiniPage = _pageFactory.Create<SkiniInfoPage>(skini);
            await Navigation.PushModalAsync(skiniPage);
        }
    }
}
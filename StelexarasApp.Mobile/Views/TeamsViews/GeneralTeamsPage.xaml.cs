using StelexarasApp.Mobile.Factories;

namespace StelexarasApp.Mobile.Views.TeamsViews;

public partial class GeneralTeamsPage : ContentPage
{
    private readonly TomeasViewModel _tomeas2ViewModel;
    private readonly TomeasViewModel _tomeas1ViewModel;
    private readonly KoinotitaViewModel _koinotitaViewModel;
    private readonly SxoliViewModel _sxoliViewModel;
    private readonly IPaidiaService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> _paidiaService;
    private readonly ITeamsService _teamsService;
    private readonly IPageFactory pageFactory;

    public GeneralTeamsPage(IPaidiaService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> paidiaService, ITeamsService teamsService, IPageFactory pageFactory)
    {
        InitializeComponent();
        _paidiaService = paidiaService ?? throw new ArgumentNullException(nameof(paidiaService));
        _teamsService = teamsService ?? throw new ArgumentNullException(nameof(teamsService));

        _tomeas1ViewModel = new TomeasViewModel(1, _teamsService, _paidiaService);
        _tomeas2ViewModel = new TomeasViewModel(2, _teamsService, _paidiaService);
        _koinotitaViewModel = new KoinotitaViewModel(_paidiaService, _teamsService);
        _sxoliViewModel = new SxoliViewModel(_teamsService, _paidiaService);
        this.pageFactory = pageFactory ?? throw new ArgumentNullException(nameof(pageFactory));
    }

    private async Task<TomeasDtoBase> GetTomea(int num)
    {
        return await _teamsService.GetTomeaByNameInService(new(), num.ToString());
    }

    private async void TomeasA_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TomeasInfoPage(_tomeas1ViewModel, _koinotitaViewModel, _teamsService, _paidiaService, pageFactory));
    }

    private async void TomeasB_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TomeasInfoPage(_tomeas2ViewModel, _koinotitaViewModel, _teamsService, _paidiaService, pageFactory));
    }

    private async void Sxoli_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SxoliInfoPage(_paidiaService, _sxoliViewModel));
    }
}
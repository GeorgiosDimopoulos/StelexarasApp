using StelexarasApp.Library.Models.Domi;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.TeamsViewModels;

namespace StelexarasApp.Mobile.Views.TeamsViews;

public partial class TomeasInfoPage : ContentPage
{
    private readonly TomeasViewModel _tomeasViewModel;
    private readonly KoinotitaViewModel _koinotitaViewModel;
    private readonly IPaidiaService _paidiaService;
    private readonly ITeamsService _teamsService;

    public TomeasInfoPage(TomeasViewModel tomeasViewModel, KoinotitaViewModel koinotitaViewModel, ITeamsService teamsService, IPaidiaService paidiaService)
    {
        InitializeComponent();
        _teamsService = teamsService;
        _paidiaService = paidiaService;
        _tomeasViewModel = tomeasViewModel;
        _koinotitaViewModel = koinotitaViewModel;
        Title = "Τομέας: " + _tomeasViewModel.TomeasNumber.ToString();
        BindingContext = _tomeasViewModel;
    }

    private async void KoinotitaButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var koinotita = (Koinotita)button.BindingContext;
        _koinotitaViewModel.Koinotita = koinotita;
        _ = Navigation.PushAsync(new KoinotitaInfoPage(_teamsService, _paidiaService, _koinotitaViewModel));
    }

    private async void OnAddKoinotitaClicked(object sender, EventArgs e)
    {
        await DisplayAlert("ΠΡΟΣΘΗΚΗ", "Δε μπορείτε να προσθέσετε νέα κοινότητα! Μονο απο την διαχείρηση γίνεται!", "OK");
    }

    private async void OnAddKoinotitaClickedAlternative(object sender, EventArgs e)
    {
        var koinotitaName = await GetValidKoinotitaName();
        if (string.IsNullOrEmpty(koinotitaName))
            return;

        var newKoinotita = new KoinotitaDto
        {
            TomeasName = _tomeasViewModel.TomeasNumber.ToString(),
            Name = koinotitaName,
            SkinesNumber = 0,

        };

        if(await _koinotitaViewModel.AddKoinotita(newKoinotita))
            await DisplayAlert("ΠΡΟΣΘΗΚΗ", "Δημιουργηθηκε επιτυχώς νέα κοινότητα!", "OK");
        else
            await DisplayAlert("ΠΡΟΣΘΗΚΗ", "Αποτυχία δημιουργίας νέας κοινότητας!", "OK");
    }

    private async Task<string> GetValidKoinotitaName()
    {
        string koinotita;
        while (true)
        {
            koinotita = await DisplayPromptAsync("Νέα Κοινότητα", "Γράψτε Κοινότητα:", "OK", "Άκυρο", "Κοινότητα", maxLength: 50, keyboard: Keyboard.Text);

            if (string.IsNullOrEmpty(koinotita))
                return null!;
            break;
        }
        return koinotita;
    }

    //private async Task<string> GetValidKoinotarxis()
    //{
    //    var yesOrNo = await DisplayAlert("Κοινοτάρχης", "Είστε σίγουροι ότι θέλετε να προσθέσετε νέο κοινοτάρχη ή θέλετε αργότερα;", "Ναι", "Όχι");
    //    if (!yesOrNo)
    //        return null!;

    //    string koinotarxis;
    //    while (true)
    //    {
    //        koinotarxis = await DisplayPromptAsync("Κοινοτάρχης", "Γράψτε Όνομα Κοινοτάρχη:", "OK", "Άκυρο", "Κοινοταρχης", maxLength: 50, keyboard: Keyboard.Text);

    //        if (string.IsNullOrEmpty(koinotarxis))
    //            return null!;
    //        break;
    //    }
    //    return koinotarxis;
    //}
}
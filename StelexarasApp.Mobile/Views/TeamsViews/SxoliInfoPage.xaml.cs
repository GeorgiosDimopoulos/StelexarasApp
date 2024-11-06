using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.UI.Views.PaidiaViews;
using StelexarasApp.ViewModels.TeamsViewModels;

namespace StelexarasApp.UI.Views.TeamsViews;

public partial class SxoliInfoPage : ContentPage
{
    private readonly SxoliViewModel _sxoliViewModel;
    private readonly IPaidiaService _paidiaService;

    public SxoliInfoPage(IPaidiaService paidiaService, SxoliViewModel sxoliViewModel)
    {
        InitializeComponent();
        _paidiaService = paidiaService ?? throw new ArgumentNullException(nameof(paidiaService));
        _sxoliViewModel = sxoliViewModel ?? throw new ArgumentNullException(nameof(sxoliViewModel));
        BindingContext = _sxoliViewModel ?? throw new ArgumentNullException(nameof(_sxoliViewModel));
    }

    private async void PaidiButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var paidi = button?.CommandParameter as PaidiDto;

        if (paidi != null)
        {
            var paidiPage = new PaidiInfoPage(_paidiaService, paidi);
            await Navigation.PushModalAsync(paidiPage);
        }
    }

    private async void AddEkpaideuomenos(object sender, EventArgs e)
    {
        PaidiDto paidiDto = null;

        while (paidiDto == null)
        {
            var fullName = await GetValidFullName();
            if (fullName == null)
                return;

            var age = await GetValidAge();
            var skiniName = await GetValidSkiniName();

            paidiDto = new PaidiDto
            {
                FullName = fullName,
                Age = age,
                SkiniName = skiniName,
                SeAdeia = false,
                PaidiType = PaidiType.Ekpaideuomenos
            };
        }

        if (await _sxoliViewModel.AddEkpaideuomenos(paidiDto))
            await DisplayAlert("ΠΡΟΣΘΗΚΗ", "Δημιουργηθηκε επιτυχώς νέο παιδί!", "OK");
    }

    private async Task<string> GetValidFullName()
    {
        while (true)
        {
            string paidiSirName = await DisplayPromptAsync("Νέο παιδί", "Γράψτε επιθετο:", "OK", "Άκυρο", "επιθετο", maxLength: 50, keyboard: Keyboard.Text);
            string paidiFirstName = await DisplayPromptAsync("Νέο παιδί", "Γράψτε όνομα:", "OK", "Άκυρο", "Ονομα", maxLength: 50, keyboard: Keyboard.Text);

            if (string.IsNullOrEmpty(paidiSirName) || string.IsNullOrEmpty(paidiFirstName))
                return null;
            var paidiFullName = paidiFirstName + " " + paidiSirName;

            var parts = paidiFullName.Trim().Split(' ');
            if (parts.Length >= 2)
                return paidiFullName;
            else
                await DisplayAlert("Σφάλμα!", "Λάθος Στοιχεία ονοματεπωνυμου παιδιου!", "OK");
        }
    }

    private async Task<int> GetValidAge()
    {
        while (true)
        {
            string paidiAge = await DisplayPromptAsync("Νέο παιδί", "Γράψτε ηλικία παιδιού:", "OK", "Άκυρο", maxLength: 2, keyboard: Keyboard.Numeric);
            if (int.TryParse(paidiAge, out int age) && age >= 6 && age <= 15)
                return age;
            else
                await DisplayAlert("Σφάλμα!", "Λάθος Στοιχεία ηλικίας παιδιου!", "OK");
        }
    }

    private async Task<string> GetValidSkiniName()
    {
        while (true)
        {
            string paidiSkiniName = await DisplayPromptAsync("Νέο παιδί", "Γράψτε όνομα σκηνής:", "OK", "Άκυρο", "Σκηνή", maxLength: 50, keyboard: Keyboard.Text);

            if (string.IsNullOrEmpty(paidiSkiniName))
                await DisplayAlert("Σφάλμα!", "Λάθος Στοιχεία σκηνής παιδιου!", "OK");
            else
                return paidiSkiniName;
        }
    }
}

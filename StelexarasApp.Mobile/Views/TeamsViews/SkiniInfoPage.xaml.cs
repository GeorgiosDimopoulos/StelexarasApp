namespace StelexarasApp.Mobile.Views.TeamsViews;

public partial class SkiniInfoPage : ContentPage
{
    private readonly SkiniViewModel _skiniViewModel;
    private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiaService;

    public SkiniInfoPage(SkiniResponse skini, IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> paidiaService)
    {
        InitializeComponent();
        _skiniViewModel = new SkiniViewModel(skini, paidiaService);
        _paidiaService = paidiaService;
    }

    private async void PaidiButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button?.CommandParameter is PaidiResponse paidi)
        {
            var paidiPage = new PaidiInfoPage(_paidiaService, paidi);
            await Navigation.PushModalAsync(paidiPage);
        }
    }

    private async void OnAddPaidiClicked(object sender, EventArgs e)
    {
        var fullName = await GetValidFullName();
        if (fullName == null)
            return;

        var age = await GetValidAge();
        var skiniName = await GetValidSkiniName();

        var paidiDto = new CreatePaidiRequest
        {
            FullName = fullName,
            Age = age,
            SkiniName = skiniName,
            SeAdeia = false,
            PaidiType = PaidiType.Kataskinotis
        };

        if (await _skiniViewModel.AddPaidiAsync(paidiDto))
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
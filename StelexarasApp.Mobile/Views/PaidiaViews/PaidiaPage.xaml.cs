namespace StelexarasApp.Mobile.Views.PaidiaViews
{
    public partial class PaidiaPage : ContentPage
    {
        private readonly IPaidiaService _paidiService;
        private readonly PaidiaViewModel paidiaViewModel;

        public PaidiaPage(IPaidiaService paidiService)
        {
            InitializeComponent();
            _paidiService = paidiService;
            BindingContext = paidiaViewModel = new PaidiaViewModel(_paidiService);
        }

        private void OnPaidiSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedPaidi = e.CurrentSelection.FirstOrDefault() as PaidiDtoBase;
            if (selectedPaidi != null)
            {
                // ToDo: Handle the selection, e.g., navigate to a detail page
            }
        }

        private void AddChild_Clicked(object sender, EventArgs e)
        {
            // ToDo: Handle the add button click
        }
    }
}
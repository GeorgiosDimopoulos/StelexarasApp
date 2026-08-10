namespace StelexarasApp.Mobile.Views.PaidiaViews
{
    public partial class PaidiaPage : ContentPage
    {
        private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> _peopleService;
        private readonly PaidiaViewModel paidiaViewModel;

        public PaidiaPage(IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> peopleService)
        {
            InitializeComponent();
            _peopleService = peopleService;
            BindingContext = paidiaViewModel = new PaidiaViewModel(_peopleService);
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
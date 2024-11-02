using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.ViewModels.PeopleViewModels;

namespace StelexarasApp.UI.Views.PaidiaViews
{
    public partial class PaidiaPage : ContentPage
    {
        private readonly IPaidiaService _peopleService;
        private readonly PaidiaViewModel paidiaViewModel;

        public PaidiaPage(IPaidiaService peopleService)
        {
            InitializeComponent();
            _peopleService = peopleService;
            BindingContext = paidiaViewModel = new PaidiaViewModel(_peopleService);
        }

        private void OnPaidiSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedPaidi = e.CurrentSelection.FirstOrDefault() as PaidiDto;
            if (selectedPaidi != null)
            {
                // ToDo: Handle the selection, e.g., navigate to a detail page
            }
        }
    }
}
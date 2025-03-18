using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Mobile.ViewModels.PeopleViewModels;
using StelexarasApp.Library.Dtos.Atoma;

namespace StelexarasApp.Mobile.Views.PaidiaViews
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

        private void AddChild_Clicked(object sender, EventArgs e)
        {
            // ToDo: Handle the add button click
        }
    }
}
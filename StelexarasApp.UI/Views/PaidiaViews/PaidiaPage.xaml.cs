using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.DtosModels;

namespace StelexarasApp.UI.Views.PaidiaViews
{
    public partial class PaidiaPage : ContentPage
    {
        public PaidiaPage(IPaidiaService peopleService)
        {
            InitializeComponent();
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
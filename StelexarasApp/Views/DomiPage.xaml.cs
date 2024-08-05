using StelexarasApp.Services.ViewModels;

namespace StelexarasApp.Presentation.Views
{
    public partial class DomiPage : ContentPage
    {
        public DomiPage()
        {
            InitializeComponent();
            BindingContext = new DomiViewModel();
        }       
    }
}

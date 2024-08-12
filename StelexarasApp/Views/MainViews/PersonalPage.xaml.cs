using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.UI.Views
{
    public partial class PersonalPage : ContentPage
    {
        private IPersonalService _personalService;
        private PersonalViewModel _personalViewModel;

        public PersonalPage(IPersonalService personalService)
        {
            _personalService = personalService;
            InitializeComponent();
            _personalViewModel = new PersonalViewModel(personalService);
            BindingContext = _personalViewModel;
        }
    }
}

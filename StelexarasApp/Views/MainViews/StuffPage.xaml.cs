using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.UI.Views
{
    public partial class StuffPage : ContentPage
    {
        private IStelexiService _personalService;
        private StaffViewModel _personalViewModel;

        public StuffPage(IStelexiService personalService, Thesi thesi)
        {
            _personalService = personalService;
            InitializeComponent();
            _personalViewModel = new StaffViewModel(personalService, thesi);
            BindingContext = _personalViewModel;
        }
    }
}

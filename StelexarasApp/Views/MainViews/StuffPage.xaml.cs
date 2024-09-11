using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.UI.Views
{
    public partial class StuffPage : ContentPage
    {
        private IStaffService _personalService;
        private StaffViewModel _personalViewModel;

        public StuffPage(IStaffService personalService, Thesi thesi)
        {
            _personalService = personalService;
            InitializeComponent();
            _personalViewModel = new StaffViewModel(personalService, thesi);
            BindingContext = _personalViewModel;
        }
    }
}

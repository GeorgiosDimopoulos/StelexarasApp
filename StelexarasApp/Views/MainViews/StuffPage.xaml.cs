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
            _personalViewModel = new StaffViewModel(personalService, GetThesiValue(thesi));
            BindingContext = _personalViewModel;
        }

        public string GetThesiValue(Thesi thesi)
        {
            return thesi switch
            {
                Thesi.Tomearxis => "Τομεάρχης",
                Thesi.Ekpaideutis => "Εκπαιδευτής",
                Thesi.Omadarxis => "Ομαδάρχης",
                Thesi.Koinotarxis => "Κοινοτάρχης",
                _ => "Unknown Thesi Title"
            };
        }
    }
}

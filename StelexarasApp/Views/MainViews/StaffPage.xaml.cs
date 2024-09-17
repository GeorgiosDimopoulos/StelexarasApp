using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.UI.Views.SecondViews;
using StelexarasApp.ViewModels;

namespace StelexarasApp.UI.Views
{
    public partial class StaffPage : ContentPage
    {
        private IStaffService _personalService;
        private StaffViewModel _personalViewModel;

        public StaffPage(IStaffService personalService, Thesi thesi)
        {
            _personalService = personalService;
            InitializeComponent();
            _personalViewModel = new StaffViewModel(personalService, GetThesiValue(thesi));
            BindingContext = _personalViewModel;
        }

        private async void OnStelexosSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var staffService = DependencyService.Get<IStaffService>();

                var selectedStaff = e.CurrentSelection [0] as StelexosDto;
                if (selectedStaff != null)
                {
                    var stelexosInfoPage = new StelexosInfoPage(staffService, selectedStaff);
                    await Navigation.PushAsync(stelexosInfoPage);
                }
            }
        }

        public string GetThesiValue(Thesi thesi)
        {
            return thesi switch
            {
                Thesi.Tomearxis => "Τομεάρχες",
                Thesi.Ekpaideutis => "Εκπαιδευτές",
                Thesi.Omadarxis => "Ομαδάρχες",
                Thesi.Koinotarxis => "Κοινοτάρχες",
                _ => "Unknown Thesi Title"
            };
        }
    }
}

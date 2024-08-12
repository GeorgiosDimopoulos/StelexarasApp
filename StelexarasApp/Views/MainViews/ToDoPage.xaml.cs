using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.UI.Views
{
    public partial class ToDoPage : ContentPage
    {
        private DutyViewModel _dutyViewModel;
        private IDutyService _dutyService;

        public ToDoPage(IDutyService dutyService)
        {
            _dutyService = dutyService;
            InitializeComponent();
            BindingContext = new DutyViewModel(_dutyService);
        }
    }
}

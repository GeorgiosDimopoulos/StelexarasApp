using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.UI.Views
{
    public partial class ToDoPage : ContentPage
    {
        private readonly IDutyService _dutyService;

        public ToDoPage(IDutyService dutyService)
        {
            _dutyService = dutyService;
            InitializeComponent();
            BindingContext = new DutyViewModel(_dutyService);
        }
    }
}

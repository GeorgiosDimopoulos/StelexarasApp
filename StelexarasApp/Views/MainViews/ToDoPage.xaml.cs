using StelexarasApp.Services.ViewModels;

namespace StelexarasApp.Presentation.Views
{
    public partial class ToDoPage : ContentPage
    {
        public ToDoPage()
        {
            InitializeComponent();
            BindingContext = new DutyViewModel();
        }
    }
}

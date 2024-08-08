using StelexarasApp.ViewModels;

namespace StelexarasApp.UI.Views
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

using StelexarasApp.Services.ViewModels;

namespace StelexarasApp.Presentation.Views
{
    public partial class ExpensesPage : ContentPage
    {
        public ExpensesPage()
        {
            InitializeComponent();
            BindingContext = new ExpensesViewModel();
        }

        private void OnAddExpenseClicked(object sender, EventArgs e)
        {
        }
    }
}

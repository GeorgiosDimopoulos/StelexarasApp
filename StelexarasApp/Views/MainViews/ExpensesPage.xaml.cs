using StelexarasApp.ViewModels;
using StelexarasApp.DataAccess.Models;
namespace StelexarasApp.UI.Views
{
    public partial class ExpensesPage : ContentPage
    {
        private ExpensesViewModel _viewModel;

        public ExpensesPage()
        {
            InitializeComponent();
            _viewModel = new ExpensesViewModel();
            BindingContext = _viewModel;
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            try
            {
                string expenseName = await DisplayPromptAsync(title: "Νέο προϊόν", message: "Όνομα προϊόντος:", accept: "OK");
                string expensePriceStr = await DisplayPromptAsync(title: "Νέο προϊόν", message: "Όνομα προϊόντος:", accept: "OK");
                
                var expensePrice = int.Parse(expensePriceStr);
                _viewModel.AddExpense(expenseName, expensePrice);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Λάθος Στοιχεία", ex.Message, "OK");
            }
        }
    }
}

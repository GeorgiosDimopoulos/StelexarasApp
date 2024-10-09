using StelexarasApp.ViewModels;
using StelexarasApp.Services.Services.IServices;
namespace StelexarasApp.UI.Views
{
    public partial class ExpensesPage : ContentPage
    {
        private readonly ExpensesViewModel _viewModel;

        public ExpensesPage(IExpenseService expenseService)
        {
            InitializeComponent();
            _viewModel = new ExpensesViewModel(expenseService);
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

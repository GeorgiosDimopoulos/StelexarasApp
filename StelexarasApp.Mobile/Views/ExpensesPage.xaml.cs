using StelexarasApp.ViewModels;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Library.Models;
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

        private async void OnExpenseTapped(object sender, SelectionChangedEventArgs e)
        {
            var selected= e.CurrentSelection.FirstOrDefault() as Expense;
            if (selected == null)
                return;

            string action = await DisplayActionSheet("Επιλογές", "Ακύρωση", null, "Διαγραφή", "Μετονομασία");
            switch (action)
            {
                case "Διαγραφή":
                    bool confirmDelete = await DisplayAlert("Διαγραφή", $"Σίγουρος για τη διαγραφή: '{selected.Description}';", "Ναι", "Όχι");
                    if (confirmDelete)
                    {
                        try
                        {
                            await _viewModel.DeleteExpense(selected.Id);
                            await DisplayAlert("Επιτυχής Διαγραφή", "Διαγραφή εξοδου!", "OK");
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                        }
                    }
                    break;

                case "Μετονομασία":
                    string newName = await DisplayPromptAsync("Μετονομασία", "Νέο όνομα εξοδου:", "OK", "Ακύρωση", initialValue: selected.Description);
                    if (!string.IsNullOrEmpty(newName) && newName != selected.Description)
                    {
                        try
                        {
                            await _viewModel.UpdateExpense(selected, newName);
                            await DisplayAlert("Επιτυχής Μετονομασία", "To εξοδο μετονομάστηκε!", "OK");
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                        }
                    }
                    break;
            }
    
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StelexarasApp.Library.Models;

namespace StelexarasApp.Mobile.ViewModels
{
    public class ExpensesViewModel(IExpenseService expenseService) : INotifyPropertyChanged
    {
        private IExpenseService _expenseService = expenseService;

        public ObservableCollection<ExpenseResponse> Expenses { get; set; } = [];
        public string StatusMessage { get; set; } = string.Empty;

        public async void AddExpense(string name, int price)
        {
            var result = await _expenseService.AddExpenseInService(new CreateExpenseRequest
            {
                Description = name,
                Amount = price
            });

            StatusMessage = result ? "Add successful" : "Add failed";
            OnPropertyChanged(nameof(StatusMessage));
        }

        public async Task DeleteExpense(int id)
        {
            var deleteExpenseRequest = new DeleteExpenseRequest
            {
                Id = id
            };
            await _expenseService.DeleteExpenseInService(deleteExpenseRequest);
        }

        public async Task LoadExpensesAsync()
        {
            var expenses = await _expenseService.GetExpensesInService();
            if (expenses is not null) 
            {
                Expenses = [.. expenses];
                StatusMessage = "Load successful";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task UpdateExpense(UpdateExpenseRequest selected, string newName)
        {
            selected.Description = newName;
            if (string.IsNullOrEmpty(newName) || newName == selected.Description)
                return;

            var updateExpenseRequest = new UpdateExpenseRequest
            {
                Id = selected.Id,
                Description = selected.Description,
                Amount = selected.Amount
            };
            await _expenseService.UpdateExpenseInService(selected);
        }
    } 
}

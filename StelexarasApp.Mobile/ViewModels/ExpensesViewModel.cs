using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StelexarasApp.Library.Models;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Mobile.ViewModels
{
    public class ExpensesViewModel(IExpenseService expenseService) : INotifyPropertyChanged
    {
        private IExpenseService _expenseService = expenseService;

        public ObservableCollection<Expense> Expenses { get; set; } = new ObservableCollection<Expense>();
        public string StatusMessage { get; set; } = string.Empty;

        public async void AddExpense(string name, int price)
        {
            var result = await _expenseService.AddExpenseInService(new Expense
            {
                Description = name,
                Date = DateTime.Today,
                Amount = price
            });

            StatusMessage = result ? "Add successful" : "Add failed";
            OnPropertyChanged(nameof(StatusMessage));
        }

        public async Task DeleteExpense(int id)
        {
            await _expenseService.DeleteExpenseInService(id);
        }

        public async Task LoadExpensesAsync()
        {
            var expenses = await _expenseService.GetExpensesInService();
            if (expenses is not null) 
            {
                Expenses = new ObservableCollection<Expense>(expenses);
                StatusMessage = "Load successful";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task UpdateExpense(Expense selected, string newName)
        {
            selected.Description = newName;
            await _expenseService.UpdateExpenseInService(selected.Id, selected);
        }
    } 
}

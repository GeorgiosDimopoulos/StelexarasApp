using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.ViewModels
{
    public class ExpensesViewModel : INotifyPropertyChanged
    {
        private IExpenseService _expenseService;

        public ObservableCollection<Expense> Expenses { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
        public ExpensesViewModel(IExpenseService expenseService)
        {
            _expenseService = expenseService;
            LoadExpensesAsync();
        }

        public async void AddExpense(string name, int price)
        {
            var result = await _expenseService.AddExpenseAsync(new Expense
            {
                Description = name,
                Date = DateTime.Today,
                Amount = price
            });

            StatusMessage = result ? "Add successful" : "Add failed";
            OnPropertyChanged(nameof(StatusMessage));
        }

        public void DeleteExpense(int id)
        {
            _expenseService.DeleteExpenseAsync(id);
        }

        public async Task LoadExpensesAsync()
        {
            var expenses = await _expenseService.GetExpensesAsync();
            if (expenses is not null)
                Expenses = new ObservableCollection<Expense>(expenses);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    } 
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.ViewModels
{
    public class ExpensesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Expense> Expenses { get; set; }

        public ExpensesViewModel()
        {
            Expenses = new ObservableCollection<Expense>
            {
                new Expense { Description = "Coffee", Date = new DateTime(2024, 12, 24), Amount = 2 },
                new Expense { Description = "Lunch", Date = DateTime.Today.AddDays(-1), Amount = 5 }
            };
        }

        public void AddExpense(string name, int price)
        {
            Expenses.Add(new Expense
            {
                Description = name, Date = DateTime.Today, Amount = price
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    } 
}

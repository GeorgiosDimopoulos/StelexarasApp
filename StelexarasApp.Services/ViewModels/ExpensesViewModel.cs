using System.Collections.ObjectModel;
using StelexarasApp.Library.Models;

namespace StelexarasApp.Services.ViewModels
{
    public class ExpensesViewModel
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
    }
}

using System.Collections.ObjectModel;
using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.ViewModels
{
    public class DutyViewModel
    {
        public ObservableCollection<Duty> Duties { get; set; }

        public DutyViewModel()
        {
            Duties =
            [
                new Duty { Name = "Do something"},
            ];
        }
    }
}

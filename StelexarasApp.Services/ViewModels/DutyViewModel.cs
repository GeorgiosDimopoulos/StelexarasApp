using System.Collections.ObjectModel;
using StelexarasApp.Library.Models;

namespace StelexarasApp.Services.ViewModels
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

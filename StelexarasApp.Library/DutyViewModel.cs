using System.Collections.ObjectModel;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.ViewModels
{
    public class DutyViewModel
    {
        private readonly IDutyService _dutyService;
        public ObservableCollection<Duty> Duties { get; set; }

        public DutyViewModel(IDutyService dutyService)
        {
            _dutyService = dutyService;
            
            Duties =
            [
                new Duty { Name = "Do something"},
            ];
        }
    }
}

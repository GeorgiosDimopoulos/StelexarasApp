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
            Duties = new ObservableCollection<Duty>();
        }

        public async Task<bool> AddDuty(string dutyName)
        {
            if (string.IsNullOrEmpty(dutyName))
            {
                return false;
            }

            var duty = new Duty
            {
                Name = dutyName,
                Date = DateTime.Now
            };

            var result = await _dutyService.AddDutyInDbAsync(duty);
            if (result)
                return true;

            return false;
        }

        public async Task<bool> DeleteDuty(string dutyName)
        {
            if (string.IsNullOrEmpty(dutyName))
            {
                return false;
            }
            
            var result = await _dutyService.DeleteDutyInDbAsync(dutyName);
            if (result)
                return true;

            return false;
        }

        public async Task<bool> UpdateDuty(Duty duty, string dutyNewName) 
        {
            if (dutyNewName == null)
            {
                return false;
            }

            var result = await _dutyService.UpdateDutyInDbAsync(dutyNewName, duty);
            if (result)
            {
                return true;
            }

            return false;
        }
    }
}

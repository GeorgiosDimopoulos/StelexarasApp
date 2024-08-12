using StelexarasApp.Services.IServices;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class PersonalViewModel : INotifyPropertyChanged
    {
        private readonly IPersonalService personalService1;

        public PersonalViewModel(IPersonalService personalService)
        {
            personalService1 = personalService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

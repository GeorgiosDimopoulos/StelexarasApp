using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using System.ComponentModel;

namespace StelexarasApp.ViewModels
{
    public class StaffViewModel : INotifyPropertyChanged
    {
        private readonly IStelexiService personalService1;

        private Thesi thesi;
        public Thesi Thesi1
        {
            get => thesi;
            set
            {
                thesi = value;
                
                // OnPropertyChanged();
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Title => Thesi1 switch
        {
            Thesi.Tomearxis => "Tomearxis",
            Thesi.Ekpaideutis => "Ekpaideutis",
            Thesi.Omadarxis => "Omadarxis",
            Thesi.Koinotarxis => "Koinotarxis",
            _ => "Unknown Thesi Title"
        };

        public StaffViewModel(IStelexiService personalService, Thesi thesi)
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

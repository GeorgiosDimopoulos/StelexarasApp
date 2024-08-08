using StelexarasApp.Library.Models.Atoma.Paidia;
using StelexarasApp.Library.Models.Domi;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StelexarasApp.Services.ViewModels
{
    public class ChildInfoViewModel : INotifyPropertyChanged
    {
        public Ekpaideuomenos Paidi { get; set; }
        public Skini Skini { get; set; }
        
        public ObservableCollection<Skini> Skines { get; set; }
        
        public string FullName { get; set; }
        public int Age { get; set; }

        public ChildInfoViewModel(Ekpaideuomenos paidi, ObservableCollection<Skini> skines)
        {
            Paidi = paidi;
            FullName = paidi.FullName;
            Age = paidi.Age;
            Skini = paidi.Skini;
            Skines = skines;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

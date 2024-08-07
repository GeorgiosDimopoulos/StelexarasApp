using StelexarasApp.Library.Models.Atoma.Paidia;
using StelexarasApp.Library.Models.Domi;
using System.Collections.ObjectModel;

namespace StelexarasApp.Services.ViewModels
{
    public class ChildInfoViewModel
    {
        public Ekpaideuomenos Paidi { get; set; }
        public ObservableCollection<Skini> Skines { get; set; }


        public ChildInfoViewModel(Ekpaideuomenos paidi, ObservableCollection<Skini> skines)
        {
            Paidi = paidi;
            Skines = skines;
        }
    }
}

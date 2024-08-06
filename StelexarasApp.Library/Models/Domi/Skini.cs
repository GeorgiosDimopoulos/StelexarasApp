using StelexarasApp.Library.Models.Atoma.Paidia;
using StelexarasApp.Library.Models.Atoma.Stelexi;
using System.Collections.ObjectModel;

namespace StelexarasApp.Library.Models.Domi
{
    public class Skini : Xwros
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Omadarxis Omadarxis { get; set; } // = new Omadarxis();

        public ObservableCollection<Ekpaideuomenos> Paidia { get; set; } // Paidi
        public Koinotita Koinotita { get; set; }
    }
}

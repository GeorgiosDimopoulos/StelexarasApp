using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using System.Collections.ObjectModel;

namespace StelexarasApp.DataAccess.Models.Domi
{
    public class Skini : Xwros
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Omadarxis Omadarxis { get; set; } // = new Omadarxis();

        public ObservableCollection<Ekpaideuomenos> Paidia { get; set; } // Paidi
        public Koinotita Koinotita { get; set; }
    }
}

using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using System.Collections.ObjectModel;

namespace StelexarasApp.DataAccess.Models.Domi
{
    public class Skini : Xwros
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Omadarxis Omadarxis { get; set; } = null!;
        public int OmadarxisId { get; set; }
        public ObservableCollection<Paidi> Paidia { get; set; } = null!;
        public Koinotita Koinotita { get; set; } = null!;
    }
}

using StelexarasApp.DataAccess.Models.Atoma;
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
        public ICollection<Paidi> Paidia { get; set; } = null!;
        public Koinotita Koinotita { get; set; } = null!;
    }
}

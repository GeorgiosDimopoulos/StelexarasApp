using StelexarasApp.DataAccess.Models.Atoma.Stelexi;

namespace StelexarasApp.DataAccess.Models.Domi
{
    public class Koinotita : Xwros
    {
        public int Id { get; set; }
        public string Name { get; set; }  = null!;
        public Koinotarxis Koinotarxis { get; set; } = null!;
        public int KoinotarxisId { get; set; }
        public Tomeas Tomeas  { get; set; } = null!;
        public IEnumerable<Skini> Skines { get; set; } = null!;
    }
}

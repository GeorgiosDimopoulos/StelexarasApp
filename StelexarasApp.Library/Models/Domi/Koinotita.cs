using StelexarasApp.Library.Models.Atoma.Stelexi;

namespace StelexarasApp.Library.Models.Domi
{
    public class Koinotita : Xwros
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; }  = null!;
        public Koinotarxis Koinotarxis { get; set; } = new Koinotarxis();
        public IEnumerable<Skini> Skines { get; set; } = null!;
    }
}

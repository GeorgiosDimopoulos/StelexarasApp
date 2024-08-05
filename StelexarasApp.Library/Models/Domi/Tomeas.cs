using StelexarasApp.Library.Models.Atoma.Stelexi;

namespace StelexarasApp.Library.Models.Domi
{
    public class Tomeas : Xwros
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Tomearxis Tomearxis { get; set; } = new Tomearxis();
        public IEnumerable<Koinotarxis> Koinotarxes { get; set; } = null!;
    }
}

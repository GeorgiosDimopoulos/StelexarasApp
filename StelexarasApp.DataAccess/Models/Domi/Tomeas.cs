using StelexarasApp.DataAccess.Models.Atoma.Stelexi;

namespace StelexarasApp.DataAccess.Models.Domi
{
    public class Tomeas : Xwros
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Tomearxis Tomearxis { get; set; } = new Tomearxis();
        public IEnumerable<Koinotarxis> Koinotarxes { get; set; } = null!;
    }
}

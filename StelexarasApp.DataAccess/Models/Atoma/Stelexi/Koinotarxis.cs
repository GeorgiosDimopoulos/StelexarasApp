using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Models.Atoma.Stelexi
{
    public class Koinotarxis : Stelexos
    {
        public string FullName { get; set; } = null!;
        public int Id { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public Koinotita Koinotita { get; set; } = null!;
        // public int KoinotitaId { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Koinotarxis;
        public IEnumerable<Omadarxis> Omadarxes { get; set; } = null!;
    }
}

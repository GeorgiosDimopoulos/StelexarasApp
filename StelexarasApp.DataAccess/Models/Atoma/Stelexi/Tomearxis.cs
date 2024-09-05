using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Stelexi
{
    public class Tomearxis : Stelexos
    {
        public string FullName { get; set; } = null!;

        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public Tomeas Tomeas { get; set; } = new Tomeas();
        public Thesi Thesi { get; set; } = Thesi.Tomearxis;
        // public int TomeasId { get; set; }
        public IEnumerable<Koinotarxis> Koinotarxes { get; set; } = null!;

        public Tomearxis()
        {
            Tomeas = new Tomeas();
            Thesi = Thesi.Tomearxis;
        }
    }
}

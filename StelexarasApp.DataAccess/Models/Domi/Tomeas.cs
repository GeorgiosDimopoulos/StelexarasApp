using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Domi
{
    public class Tomeas : Xwros
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Tomearxis Tomearxis { get; set; } = new Tomearxis();
        public int TomearxisId { get; set; }
        public IEnumerable<Koinotita> Koinotites { get; set; } = null!;
    }
}

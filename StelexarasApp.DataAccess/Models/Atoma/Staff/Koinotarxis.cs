using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public class Koinotarxis : StelexosBase
    {
        public new required string FullName { get; set; }

        [Key]
        public new int Id { get; set; }
        public new int Age { get; set; }
        public new Sex Sex { get; set; }

        [Required]
        public Koinotita Koinotita { get; set; } = null!;

        [Required]
        public new required string Tel { get; set; }
        public new Thesi Thesi { get; set; } = Thesi.Koinotarxis;
        public IEnumerable<Omadarxis> Omadarxes { get; set; } = null!;
    }
}

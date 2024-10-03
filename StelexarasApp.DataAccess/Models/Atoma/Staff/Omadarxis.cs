using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public class Omadarxis : StelexosBase
    {
        public new required string FullName { get; set; } = null!;

        [Key]
        public new int Id { get; set ; }
        public new int Age { get; set ; }
        public new Sex Sex { get; set ; }
        
        [Required]
        public Skini Skini { get; set; } = new Skini();
        public new Thesi Thesi { get; set; } = Thesi.Omadarxis;
        public new required string Tel { get; set; } = null!;
    }
}

using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public class Omadarxis : Stelexos
    {
        public string FullName { get; set; } = null!;

        [Key]
        public int Id { get; set ; }
        public int Age { get; set ; }
        public Sex Sex { get; set ; }
        public Skini Skini { get; set; } = new Skini();
        public Thesi Thesi { get; set; } = Thesi.Omadarxis;
        public string Tel { get; set; } = null!;
    }
}

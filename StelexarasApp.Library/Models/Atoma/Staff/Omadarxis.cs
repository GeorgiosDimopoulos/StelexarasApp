using StelexarasApp.Library.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma.Staff
{
    public class Omadarxis : IStelexos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Skini Skini { get; set; } = new Skini();
        public required string FullName { get; set; }
        public required string Tel { get; set; }
        public Thesi Thesi { get; set; }
        public string? XwrosName { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
    }
}

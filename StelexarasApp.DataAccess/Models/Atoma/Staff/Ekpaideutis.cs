using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public class Ekpaideutis : IStelexos
    {
        public required string FullName { get; set; }

        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }

        [Required]
        public string Tel { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
        public string? XwrosName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma.Staff
{
    public class Ekpaideutis : IStelexos
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public Sex Sex { get; set; }

        [Required]
        public string Tel { get; set; } = string.Empty;
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
        public string? XwrosName { get; set; }
    }
}

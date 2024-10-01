using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public class Ekpaideutis : StelexosBase
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }

        [Required]
        public string Tel { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
    }
}

using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public class Ekpaideutis : StelexosBase
    {
        public new required string FullName { get; set; } = null!;

        [Key]
        public new int Id { get; set; }
        public new int Age { get; set; }
        public new Sex Sex { get; set; }

        [Required]
        public string Tel { get; set; }
        public new Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
    }
}

using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma.Staff
{
    public class Ekpaideutis : IStelexos
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public string Tel { get; set; } = string.Empty;
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
        public string XwrosName { get; set; } = string.Empty;
    }
}

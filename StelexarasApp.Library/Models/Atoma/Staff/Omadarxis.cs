using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma.Staff
{
    public class Omadarxis : IStelexos
    {
        [Key]
        public int Id { get; set; }
        public Skini Skini { get; set; } = new Skini();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public Thesi Thesi { get; set; }
        public string XwrosName { get; set; } = string.Empty;
        public Sex Sex { get; set; }
        public int Age { get; set; }
    }
}

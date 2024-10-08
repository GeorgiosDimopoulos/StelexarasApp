using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public interface IStelexos : IPerson
    {
        public string FullName { get; set; }
        public string Tel { get; set; }

        [Key]
        public int Id { get; set; }
        public Thesi Thesi { get; set; }
        public string? XwrosName { get; set; }
        public Sex Sex { get; set; }

        public int Age { get; set; }
    }

    public enum Thesi 
    { 
        None = 0,
        Omadarxis = 1,
        Koinotarxis = 2,
        Tomearxis = 3,
        Ekpaideutis = 4,
    }
}

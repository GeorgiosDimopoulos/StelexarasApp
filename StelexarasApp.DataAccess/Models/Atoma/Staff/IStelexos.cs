using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public interface IStelexos : IPerson
    {
        public new string FullName { get; set; }
        public string Tel { get; set; }

        [Key]
        public new int Id { get; set; }
        public Thesi Thesi { get; set; }
        public string? XwrosName { get; set; }
        public new Sex Sex { get; set; }

        public new int Age { get; set; }
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

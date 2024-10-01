using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public interface IStelexos : Atomo
    {
        public new string FullName { get; set; }
        public string Tel { get; set; }
        [Key]
        public new int Id { get; set; }
        public Thesi Thesi { get; set; }
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

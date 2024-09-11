using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public interface Stelexos : Atomo
    {
        public new string FullName { get; set; }
        public int Tel { get; set; }
        [Key]
        public new int Id { get; set; }
        public Thesi Thesi { get; set; }
        // public Xwros Xwros{ get; set; }

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

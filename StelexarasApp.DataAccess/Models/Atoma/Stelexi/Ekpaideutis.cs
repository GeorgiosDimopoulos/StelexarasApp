using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Stelexi
{
    public class Ekpaideutis : Stelexos
    {
        public string FullName { get; set; } = null!;
        
        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        // public Xwros Xwros { get; set; } = null!;
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
    }
}

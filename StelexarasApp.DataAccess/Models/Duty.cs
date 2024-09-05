using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models
{
    public class Duty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }        
        public DateTime Date { get; set; }
    }
}

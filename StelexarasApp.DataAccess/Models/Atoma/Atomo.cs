using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma
{
    public interface Atomo
    {
        string FullName { get; set; }

        [Key]
        int Id { get; set; }

        int Age { get; set; }

        Sex Sex { get; set; }
    }
}
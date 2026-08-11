using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }

        [Key]
        int Id { get; set; }

        int Age { get; set; }

        Sex Sex { get; set; }
    }
}
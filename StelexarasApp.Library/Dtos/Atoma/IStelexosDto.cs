using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Atoma;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Dtos.Atoma
{
    public interface IStelexosDto
    {
        [Required]
        public string FullName { get; set; }
        public int Age { get; set; }
        public string? XwrosName { get; set; }
        [Phone]
        public string? Tel { get; set; }

        [Required]
        public Thesi Thesi { get; set; }
        public Sex Sex { get; set; }
    }
}

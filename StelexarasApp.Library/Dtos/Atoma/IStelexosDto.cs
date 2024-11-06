using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Atoma;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Dtos.Atoma
{
    public interface IStelexosDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public int Id { get; set; }
        public Thesi Thesi { get; set; }
        public string? DtoXwrosName { get; set; }

        [Phone]
        public string? Tel { get; set; }
    }
}

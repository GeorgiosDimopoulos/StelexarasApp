using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Atoma;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Services.DtosModels.Atoma
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

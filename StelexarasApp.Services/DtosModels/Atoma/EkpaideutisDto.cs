using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.DtosModels.Atoma
{
    public class EkpaideutisDto : IStelexosDto
    {
        public string? FullName { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
        public int Age { get; set; }
        public int? Id { get; set; }
        public Sex Sex { get; set; }
        public string? DtoXwrosName { get; set; }
        public string? Tel { get; set; }
        int IStelexosDto.Id { get; set; }
    }
}

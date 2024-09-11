using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.DtosModels.Atoma
{
    public class EkpaideutisDto : StelexosDto
    {
        public string? FullName { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
        public int Age { get; set; }
        public int? Id { get; set; }
        public Sex Sex { get; set; }
    }
}

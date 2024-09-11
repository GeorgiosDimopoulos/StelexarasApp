using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Services.DtosModels.Atoma
{
    public class StelexosDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public int? Id { get; set; }
        public Thesi Thesi { get; set; }
        public string XwrosName { get; set; } = string.Empty;
    }
}

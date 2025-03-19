using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;

namespace StelexarasApp.Library.Dtos.Atoma
{
    public class EkpaideutisDto : IStelexosDto
    {
        public string FullName { get; set; } = string.Empty;
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public string? XwrosName { get; set; }
        public string? Tel { get; set; }
    }
}

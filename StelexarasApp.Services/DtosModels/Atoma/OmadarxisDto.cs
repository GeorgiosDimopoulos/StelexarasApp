using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Services.DtosModels.Atoma
{
    public class OmadarxisDto : StelexosDto
    {
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public int SkiniId { get; set; }
        public int? Id { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Omadarxis;
        // public List<int> PaidiaIds { get; set; } = new List<int>();
    }
}

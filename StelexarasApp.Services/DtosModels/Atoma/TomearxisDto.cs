using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Services.DtosModels.Atoma
{
    public class TomearxisDto : StelexosDto
    {
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public int? Id { get; set; }
        public Sex Sex { get; set; }
        public int TomeasId { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Tomearxis;
        public List<int> KoinotarxesIds { get; set; } = new List<int>();
    }
}

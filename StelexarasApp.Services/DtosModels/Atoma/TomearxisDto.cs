using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Services.DtosModels.Atoma
{
    public class TomearxisDto : StelexosDto
    {
        public new required string FullName { get; set; }
        public new int Age { get; set; }
        public new int? Id { get; set; }
        public new Sex Sex { get; set; }
        public int TomeasId { get; set; }
        public new Thesi Thesi { get; set; } = Thesi.Tomearxis;
        public List<int> KoinotarxesIds { get; set; } = new List<int>();
    }
}

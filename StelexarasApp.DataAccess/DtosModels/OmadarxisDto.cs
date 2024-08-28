using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.DataAccess.DtosModels
{
    public class OmadarxisDto
    {
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public int SkiniId { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Omadarxis;
        // public List<int> PaidiaIds { get; set; } = new List<int>();
    }
}

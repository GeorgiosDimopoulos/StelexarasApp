using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.DataAccess.DtosModels
{
    public class KoinotarxisDto
    {
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public int KoinotitaId { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Koinotarxis;
        public List<int> OmadarxesIds { get; set; } = new List<int>();
    }
}

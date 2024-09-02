using StelexarasApp.Services.DtosModels.Atoma;

namespace StelexarasApp.Services.DtosModels.Domi
{
    public class TomeasDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int KoinotitesNumber { get; set; }
        public TomearxisDto? Tomearxis { get; set; }
    }
}

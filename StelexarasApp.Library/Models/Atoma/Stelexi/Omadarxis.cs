using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.Library.Models.Atoma.Stelexi
{
    public class Omadarxis : Stelexos
    {
        public string FullName { get; set; } = null!;
        public string Id { get; set ; } = null!;
        public int Age { get; set ; }
        public Sex Sex { get; set ; }
        public Skini Skini { get; set; } = new Skini();
        public int SkiniId { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Omadarxis;
    }
}

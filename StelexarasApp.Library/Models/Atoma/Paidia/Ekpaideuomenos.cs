using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.Library.Models.Atoma.Paidia
{
    public class Ekpaideuomenos : Paidi
    {
        public string FullName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public int Age { get; set; } = 16;
        public Sex Sex { get; set; }
        public Skini Skini { get; set; } // = new Skini();
    }
}

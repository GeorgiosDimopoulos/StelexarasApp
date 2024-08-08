using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Models.Atoma.Paidia
{
    public class Ekpaideuomenos : Paidi
    {
        public string FullName { get; set; } = string.Empty;
        public int Id { get; set; }
        public int Age { get; set; } = 16;
        public Sex Sex { get; set; }

        public bool SeAdeia { get; set; }
        public Skini Skini { get; set; } // = new Skini();
    }
}

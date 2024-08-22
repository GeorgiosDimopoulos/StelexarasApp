using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Models.Atoma
{
    public class Paidi : Atomo
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
        public bool SeAdeia { get; set; }
        public Sex Sex { get; set; }
        public PaidiType PaidiType { get; set; }
        public int SkiniId { get; set; }

        public Skini Skini { get; set; } = new Skini();
    }

    public enum PaidiType
    {
        Ekpaideuomenos,
        Kataskinotis
    }
}

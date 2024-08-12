using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Models.Atoma.Paidia
{
    public class Kataskinotis : Paidi
    {
        public string FullName { get; set; } = string.Empty;
        public int Id { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public Skini? Skini { get; set; }
        public PaidiType PaidiType { get; set; } = PaidiType.Kataskinotis;
    }
}

using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.Library.Models.Atoma.Stelexi
{
    public class Ekpaideutis : Stelexos
    {
        public string FullName { get; set; } = null!;
        public string Id { get; set; } = null!;
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public Tomeas Tomeas { get; set; } = new Tomeas();
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
    }
}

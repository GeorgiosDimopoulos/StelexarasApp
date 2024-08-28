using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Models.Atoma.Stelexi
{
    public class Omadarxis : Stelexos
    {
        public string FullName { get; set; } = null!;
        public int Id { get; set ; }
        public int Age { get; set ; }
        public Sex Sex { get; set ; }
        public Skini Skini { get; set; } = new Skini();
        public Thesi Thesi { get; set; } = Thesi.Omadarxis;
        
        // public List<int> PaidiaIds { get; set; } = new List<int>();
    }
}

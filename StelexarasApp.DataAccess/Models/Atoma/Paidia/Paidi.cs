using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Models.Atoma.Paidia
{
    public interface Paidi : Atomo
    {
        public string FullName { get; set; }
        public int Id { get; set; }

        public PaidiType PaidiType { get; set; }
        
        public Skini Skini { get; set; }
    }    
}

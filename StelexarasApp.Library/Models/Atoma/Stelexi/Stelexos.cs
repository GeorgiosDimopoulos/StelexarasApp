using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.Library.Models.Atoma.Stelexi
{
    public interface Stelexos : Atomo
    {
        public new string FullName { get; set; }
        public new string Id { get; set; }

        public Thesi Thesi { get; set; }
        // public Xwros Xwros { get; set; }
        // public int XwrosId { get; set; }
    }

    public enum Thesi 
    { 
        None = 0,
        Omadarxis = 1,
        Koinotarxis = 2,
        Tomearxis = 3,
        Ekpaideutis = 4,
    }
}

namespace StelexarasApp.DataAccess.Models.Atoma.Stelexi
{
    public interface Stelexos : Atomo
    {
        public new string FullName { get; set; }
        public new int Id { get; set; }
        public Thesi Thesi { get; set; }
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

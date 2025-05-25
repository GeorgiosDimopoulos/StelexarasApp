namespace StelexarasApp.Library.Models.Atoma.Staff;

public interface IStelexos : IPerson
{
    string Tel { get; set; }
    Thesi Thesi { get; set; }
    string XwrosName { get; set; }
}

public enum Thesi
{
    None = 0,
    Omadarxis = 1,
    Koinotarxis = 2,
    Tomearxis = 3,
    Ekpaideutis = 4,
}

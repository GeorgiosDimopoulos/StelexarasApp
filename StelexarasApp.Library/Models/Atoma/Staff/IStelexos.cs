namespace StelexarasApp.Library.Models.Atoma.Staff;

public interface IStelexos : IPerson
{
    public string Tel { get; set; }
    public Thesi Thesi { get; set; }
    public string? XwrosName { get; set; }
}

public enum Thesi
{
    None = 0,
    Omadarxis = 1,
    Koinotarxis = 2,
    Tomearxis = 3,
    Ekpaideutis = 4,
}

using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Domi;

public class Koinotita : Xwros
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? KoinotarxisId { get; set; }

    public HlikiaKoinotitas Hlikia { get; set; }

    public Koinotarxis? Koinotarxis { get; set; }
    public Tomeas Tomeas { get; set; } = null!;
    public List<Skini>? Skines { get; set; }
}

public enum HlikiaKoinotitas
{
    Mikra = 0,
    Mesaia = 1,
    Megala = 2,
}

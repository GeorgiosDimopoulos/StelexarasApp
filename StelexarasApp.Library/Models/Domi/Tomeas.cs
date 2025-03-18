using StelexarasApp.Library.Models.Atoma.Staff;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Domi;

public class Tomeas : Xwros
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int? TomearxisId { get; set; }
    public Tomearxis? Tomearxis { get; set; } = null!;

    public IEnumerable<Koinotita> Koinotites { get; set; } = new List<Koinotita>();
}

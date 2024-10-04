using StelexarasApp.DataAccess.Models.Atoma.Staff;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Domi;

public class Tomeas : Xwros
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }
    public Tomearxis? Tomearxis { get; set; } = null!;

    // [ForeignKey(nameof(Tomearxis))]
    public int? TomearxisId { get; set; }
    public IEnumerable<Koinotita> Koinotites { get; set; } = new List<Koinotita>();

    public Tomeas()
    {
    }
}

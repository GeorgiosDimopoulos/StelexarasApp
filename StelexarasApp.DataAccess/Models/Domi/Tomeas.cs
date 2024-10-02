using StelexarasApp.DataAccess.Models.Atoma.Staff;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StelexarasApp.DataAccess.Models.Domi;

public class Tomeas : Xwros
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public Tomearxis? Tomearxis { get; set; } = null!;

    // [ForeignKey(nameof(Tomearxis))]
    public int? TomearxisId { get; set; }
    public IEnumerable<Koinotita> Koinotites { get; set; } = Enumerable.Empty<Koinotita>();
}

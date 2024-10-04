using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff;

public class Tomearxis : StelexosBase
{
    [Required]
    public required string FullName { get; set; }

    [Key]
    public new int Id { get; set; }

    public new int Age { get; set; }

    public new Sex Sex { get; set; }

    [Required]
    public required string Tel { get; set; }

    [Required]
    public required Tomeas Tomeas { get; set; }
    public new Thesi Thesi { get; set; } = Thesi.Tomearxis;

    public IEnumerable<Koinotarxis> Koinotarxes { get; set; } = null!;
}

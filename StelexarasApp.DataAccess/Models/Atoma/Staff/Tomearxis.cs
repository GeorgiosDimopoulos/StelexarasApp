using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff;

public class Tomearxis : Stelexos
{
    public required string FullName { get; set; }

    [Key]
    public int Id { get; set; }

    public int Age { get; set; }

    public Sex Sex { get; set; }

    [Required]
    public required string Tel { get; set; }

    [Required]
    public required Tomeas Tomeas { get; set; }
    public Thesi Thesi { get; set; } = Thesi.Tomearxis;
    // public int TomeasId { get; set; }
    public IEnumerable<Koinotarxis> Koinotarxes { get; set; } = null!;
}

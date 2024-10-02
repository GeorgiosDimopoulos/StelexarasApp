using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Domi;

public class Skini : Xwros
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Omadarxis? Omadarxis { get; set; }
    public int? OmadarxisId { get; set; }
    public ICollection<Paidi> Paidia { get; set; } = [];
    public Sex Sex { get; set; }
    public Koinotita Koinotita { get; set; } = null!;
}

using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff;

public class Koinotarxis : IStelexos
{
    [Key]
    public int Id { get; set; }
    [Required]
    public Koinotita Koinotita { get; set; }
    public IEnumerable<Omadarxis> Omadarxes { get; set; }
    public required string FullName { get; set; }
    public required string Tel { get; set; }
    public required Thesi Thesi { get; set; } = Thesi.Koinotarxis;
    public string? XwrosName { get; set; }
    public required Sex Sex { get; set; }
    public required int Age { get; set; }
}
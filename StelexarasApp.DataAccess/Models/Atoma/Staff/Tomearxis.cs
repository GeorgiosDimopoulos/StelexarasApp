using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff;

public class Tomearxis : IStelexos
{

    [Key]
    public int Id { get; set; }
        
    [Required]
    public required Tomeas Tomeas { get; set; }

    public IEnumerable<Koinotarxis> Koinotarxes { get; set; }
    public required string FullName { get; set; }
    public required string Tel { get; set; }
    public Thesi Thesi { get; set; }
    public Sex Sex { get; set; }
    public int Age { get; set; }
    public string? XwrosName { get; set; }
}

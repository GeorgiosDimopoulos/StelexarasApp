using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma.Staff;

public class Koinotarxis : IStelexos
{
    [Key]
    public int Id { get; set; }
    public Koinotita Koinotita { get; set; } = default!;
    public IEnumerable<Omadarxis> Omadarxes { get; set; } = default!;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Tel { get; set; } = string.Empty;
    public Thesi Thesi { get; set; } = Thesi.Koinotarxis;
    public string XwrosName { get; set; } = string.Empty;
    public Sex Sex { get; set; }
    public int Age { get; set; }
}
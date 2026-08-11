using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma.Staff;

public class Tomearxis : IStelexos
{

    [Key]
    public int Id { get; set; }

    public Tomeas Tomeas { get; set; } = default!;

    public IEnumerable<Koinotarxis>? Koinotarxes { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Tel { get; set; } = string.Empty;
    public Thesi Thesi { get; set; } = Thesi.Tomearxis;
    public Sex Sex { get; set; }
    public int Age { get; set; }
    public string XwrosName { get; set; } = string.Empty;
}

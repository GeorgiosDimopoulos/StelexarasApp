using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StelexarasApp.Library.Dtos.Atoma;

public class OmadarxisDto : IStelexosDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int Age { get; set; } = default!;
    public Sex Sex { get; set; } = default!;
    public Thesi Thesi { get; set; } = Thesi.Omadarxis;
    public string? XwrosName { get; set; }
    public string? Tel { get; set; }
}

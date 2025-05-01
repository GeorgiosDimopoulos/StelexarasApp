using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StelexarasApp.Library.Dtos.Atoma;

public class KoinotarxisDto : IStelexosDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int Age { get; set; } = default!;
    public Sex Sex { get; set; } = default!;
    public Thesi Thesi { get; set; } = Thesi.Koinotarxis;
    public string? XwrosName { get; set; } = default!;
    public string? Tel { get; set; } = default!;
}
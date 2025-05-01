using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Dtos.People;

public class EkpaideutisDto : IStelexosDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string? XwrosName { get; set; }
    public string? Tel { get; set; }
}

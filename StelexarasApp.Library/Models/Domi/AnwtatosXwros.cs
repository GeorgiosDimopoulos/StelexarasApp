using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Domi;

public class AnwtatosXwros : Xwros
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Anwtatos? Anwtatos { get; set; }
    public int? AnwtatosId { get; set; }
}

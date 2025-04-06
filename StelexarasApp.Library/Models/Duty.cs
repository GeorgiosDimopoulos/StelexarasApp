using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models;

public class Duty
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}

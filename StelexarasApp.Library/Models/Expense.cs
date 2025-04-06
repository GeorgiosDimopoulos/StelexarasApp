using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models;

public class Expense
{
    [Key]
    public int Id { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; } = string.Empty;

    public DateTime? Date { get; set; }
}

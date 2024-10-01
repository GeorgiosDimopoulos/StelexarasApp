using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StelexarasApp.DataAccess.Models;

public class Expense
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }

    public DateTime Date { get; set; }
}

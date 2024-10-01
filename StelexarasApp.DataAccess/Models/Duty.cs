using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StelexarasApp.DataAccess.Models;

public class Duty
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
}

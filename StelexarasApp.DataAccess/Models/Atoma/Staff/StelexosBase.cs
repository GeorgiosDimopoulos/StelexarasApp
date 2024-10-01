namespace StelexarasApp.DataAccess.Models.Atoma.Staff;

public abstract class StelexosBase : IStelexos
{
    public string? FullName { get; set; }
    public string? Tel { get; set; }
    public int Id { get; set; }
    public Thesi Thesi { get; set; }
    public Sex Sex { get; set; }
    public int Age { get; set; }
    public string? XwrosName { get; set; }
}

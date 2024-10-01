namespace StelexarasApp.DataAccess.Models.Atoma.Staff;

public abstract class StelexosBase : IStelexos
{
    public string FullName { get; set; } = string.Empty;
    public string Tel { get; set; } = string.Empty;
    public int Id { get; set; }
    public Thesi Thesi { get; set; }
    public Sex Sex { get; set; }
    public int Age { get; set; }
}

using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;

namespace StelexarasApp.Library.Dtos.Atoma;

public class TomearxisDto : IStelexosDto
{
    public List<int>? KoinotarxesIds { get; set; }
    public string? FullName { get; set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public Thesi Thesi { get; set; }
    public string? XwrosName { get; set; }
    public string? Tel { get; set; }
}

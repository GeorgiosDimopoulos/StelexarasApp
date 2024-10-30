using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.DtosModels.Atoma;

public class OmadarxisDto : IStelexosDto
{
    public int SkiniId { get; set; } = default;
    public string? FullName { get; set; }
    public int Age { get; set; } = default!;
    public Sex Sex { get; set; } = default!;
    public int Id { get; set; } = default!;
    public Thesi Thesi { get; set; } = default!;
    public string? DtoXwrosName { get; set; }   
    public string? Tel { get; set; } 
}

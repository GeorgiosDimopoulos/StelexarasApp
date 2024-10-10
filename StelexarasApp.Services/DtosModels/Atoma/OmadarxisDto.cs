using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.DtosModels.Atoma;

public class OmadarxisDto : IStelexosDto
{
    public int SkiniId { get; set; }
    public string? FullName { get; set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public int Id { get; set; }
    public Thesi Thesi { get; set; }
    public string? DtoXwrosName { get; set; }
    public string? Tel { get; set; }
}

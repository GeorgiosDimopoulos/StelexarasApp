using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers.StaffControllers;

[ApiController]
[Route("[controller]")]
public class StaffController(IStaffService stelexiService) : ControllerBase
{
    private readonly IStaffService _stelexiService = stelexiService;
        
    [HttpGet("StelexosByName")]
    public async Task<ActionResult<IStelexosDto>> GetStelexosByNameInService(string name, string? thesiStr)
    {
        var thesi = Thesi.None;
        if (thesiStr is not null)
        {
            thesi = thesiStr switch
            {
                "Omadarxis" => Thesi.Omadarxis,
                "Koinotarxis" => Thesi.Koinotarxis,
                "Tomearxis" => Thesi.Tomearxis,
                "Ekpaideutis" => Thesi.Ekpaideutis,
                _ => Thesi.None
            };
        }

        var result = await _stelexiService.GetStelexosByNameInService(name, thesi);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
}

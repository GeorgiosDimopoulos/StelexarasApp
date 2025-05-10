using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.ApiControllers.PeopleControllers.StaffControllers;

[ApiController]
[Route("[controller]")]
public class StaffController(IStaffService<IStelexos, IStelexosDto, IStelexosDto, IStelexosDto> stelexiService) : ControllerBase
{
    private readonly IStaffService<IStelexos, IStelexosDto, IStelexosDto, IStelexosDto>  _stelexiService = stelexiService;
        
    //[HttpGet("StelexosByName")]
    //public async Task<ActionResult<IStelexosDto>> GetStelexosByNameInService(string name, string? thesiStr)
    //{
    //    var thesi = Thesi.None; 
    //    if (thesiStr is not null)
    //    {
    //        thesi = thesiStr switch
    //        {
    //            "Omadarxis" => Thesi.Omadarxis,
    //            "Koinotarxis" => Thesi.Koinotarxis,
    //            "Tomearxis" => Thesi.Tomearxis,
    //            "Ekpaideutis" => Thesi.Ekpaideutis,
    //            _ => Thesi.None
    //        };
    //    }

    //    var result = await _stelexiService.GetStelexosByNameInService(name, thesi);
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}
}

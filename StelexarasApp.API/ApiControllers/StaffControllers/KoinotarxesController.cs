using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers.StaffControllers;

[ApiController]
[Route("[controller]")]
public class KoinotarxesController(IStaffService stelexiService) : ControllerBase
{
    private readonly IStaffService _stelexiService = stelexiService;

    [HttpGet("Koinotarxes")]
    public async Task<ActionResult<IEnumerable<KoinotarxisDto>>> GetKoinotarxes()
    {
        var result = await _stelexiService.GetAllKoinotarxesInService();
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Koinotarxi/{id}")]
    public async Task<ActionResult<Koinotarxis>> GetKoinotarxis(int id)
    {
        var result = await _stelexiService.GetStelexosByIdInService(id);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("KoinotarxesTomea/{id}")]
    public async Task<ActionResult<OmadarxisDto>> GetKoinotarxesAnaTomea([FromBody] TomeasDto tomea)
    {
        var result = await _stelexiService.GetKoinotarxesSeTomeaInService(tomea);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("Koinotarxi")]
    public async Task<ActionResult<Koinotarxis>> PostKoinotarxi([FromBody] IStelexosDto koinotarxis)
    {
        if (koinotarxis == null)
        {
            return BadRequest("Koinotarxis parameters cannot be null");
        }

        koinotarxis.Thesi = Thesi.Koinotarxis;
        var result = await _stelexiService.AddStelexosInService(koinotarxis);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("Koinotarxi/{id}")]
    public async Task<IActionResult> PutKoinotarxi([FromBody] KoinotarxisDto koinotarxisDto, int id)
    {
        if (koinotarxisDto == null)
        {
            return BadRequest("Koinotarxis cannot be null");
        }

        var result = await _stelexiService.UpdateStelexosInService(id, koinotarxisDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("Koinotarxi/{id}")]
    public async Task<IActionResult> DeleteKoinotarxi(int id)
    {
        var result = await _stelexiService.DeleteStelexosByIdInService(id);

        if (!result)
            return NotFound();
        return Ok(result);
    }
}

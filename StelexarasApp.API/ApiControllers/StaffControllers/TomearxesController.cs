using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers.StaffControllers;

[ApiController]
[Route("[controller]")]
public class TomearxesController(IStaffService stelexiService) : ControllerBase
{
    private readonly IStaffService _stelexiService = stelexiService;

    [HttpGet("Tomearxes")]
    public async Task<ActionResult<IEnumerable<Tomearxis>>> GetTomearxes()
    {
        var result = await _stelexiService.GetAllTomearxesInService();

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Tomearxi/{id}")]
    public async Task<ActionResult<Tomearxis>> GetTomearxis(int id)
    {
        var result = await _stelexiService.GetStelexosByIdInService(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("Tomearxi")]
    public async Task<ActionResult<Tomearxis>> PostTomearxi([FromBody] TomearxisDto tomearxisDto)
    {
        if (tomearxisDto == null)
        {
            return BadRequest("Tomearxis input cannot be null");
        }

        var result = await _stelexiService.AddStelexosInService(tomearxisDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("Tomearxi/{id}")]
    public async Task<IActionResult> PutTomearxi([FromBody] TomearxisDto tomearxisDto)
    {
        if (tomearxisDto == null)
        {
            return BadRequest("Tomearxis cannot be null");
        }

        var result = await _stelexiService.UpdateStelexosInService(tomearxisDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("Tomearxi/{id}")]
    public async Task<IActionResult> DeleteTomearxi(int id)
    {
        var result = await _stelexiService.DeleteStelexosByIdInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }
}

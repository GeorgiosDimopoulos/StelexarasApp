using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.QueryParameters;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers.StaffControllers;

[ApiController]
[Route("[controller]")]
public class TomearxesController(IStaffService stelexiService) : ControllerBase
{
    private readonly IStaffService _stelexiService = stelexiService;

    [HttpGet("Tomearxes")]
    public async Task<ActionResult<IEnumerable<TomearxisDto>>> GetTomearxes(TomearxisQueryParameters queryParameters)
    {
        var result = await _stelexiService.GetAllTomearxesInService(queryParameters);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Tomearxi/{id}")]
    public async Task<ActionResult<TomearxisDto>> GetTomearxis(int id)
    {
        var result = await _stelexiService.GetStelexosByIdInService(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("Tomearxi")]
    public async Task<ActionResult<TomearxisDto>> PostTomearxi([FromBody] TomearxisDto tomearxisDto)
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
    public async Task<IActionResult> PutTomearxi(int id, [FromBody] TomearxisDto tomearxisDto)
    {
        if (tomearxisDto == null)
        {
            return BadRequest("Tomearxis cannot be null");
        }

        var result = await _stelexiService.UpdateStelexosInService(id, tomearxisDto);

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

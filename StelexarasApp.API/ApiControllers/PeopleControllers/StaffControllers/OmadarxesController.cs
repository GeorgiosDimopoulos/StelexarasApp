using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.ApiControllers.PeopleControllers.StaffControllers;

[ApiController]
[Route("[controller]")]
public class OmadarxesController(IStaffService stelexiService) : ControllerBase
{
    private readonly IStaffService _stelexiService = stelexiService;

    [HttpGet("Omadarxes")]
    public async Task<ActionResult<IEnumerable<IStelexos>>> GetOmadarxes([FromQuery] OmadarxisQueryParameters queryParameters)
    {
        var result = await _stelexiService.GetAllOmadarxesInService(queryParameters);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Omadarxi/{id}")]
    public async Task<ActionResult<Omadarxis>> GetOmadarxis(int id)
    {
        var result = await _stelexiService.GetStelexosByIdInService(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPost("Omadarxi")]
    public async Task<ActionResult<Omadarxis>> PostOmadarxi([FromBody] CreateOmadarxisRequest omadarxis)
    {
        if (omadarxis == null)
        {
            return BadRequest("Omadarxis parameters cannot be null");
        }

        var result = await _stelexiService.AddStelexosInService(omadarxis);

        if (!result)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new Omadarxis record");
        }

        return CreatedAtAction(nameof(GetOmadarxis), new { Name = omadarxis.FullName }, omadarxis);
    }


    [HttpGet("OmadarxesKoinotitas/{name}")]
    public async Task<ActionResult<OmadarxisResponse>> GetOmadarxesAnaKoinotita(string name, [FromBody] OmadarxisQueryParameters queryParameters)
    {
        var result = await _stelexiService.GetOmadarxesSeKoinotitaInService(name, queryParameters);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("OmadarxesTomea/{name}")]
    public async Task<ActionResult<OmadarxisResponse>> GetOmadarxesAnaTomea(string name, [FromBody] OmadarxisQueryParameters queryParameters)
    {
        var result = await _stelexiService.GetOmadarxesSeTomeaInService(name, queryParameters);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPut("Omadarxi/{id}")]
    public async Task<IActionResult> PutOmadarxi(int id, [FromBody] UpdateOmadarxisRequest omadarxisDto)
    {
        var result = await _stelexiService.UpdateStelexosInService(id, omadarxisDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("Omadarxi/{id}")]
    public async Task<IActionResult> DeleteOmadarxi(int id)
    {
        var result = await _stelexiService.DeleteStelexosByIdInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }
}

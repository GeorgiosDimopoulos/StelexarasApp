using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.QueryParameters;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers.StaffControllers;

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

    [HttpPost("Omadarxi")]
    [Authorize]
    public async Task<ActionResult<Omadarxis>> PostOmadarxi([FromBody] IStelexosDto omadarxis)
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


    [HttpGet("OmadarxesKoinotitas/{id}")]
    public async Task<ActionResult<OmadarxisDto>> GetOmadarxesAnaKoinotita([FromBody] KoinotitaDto koinotita, OmadarxisQueryParameters queryParameters)
    {
        var result = await _stelexiService.GetOmadarxesSeKoinotitaInService(koinotita, queryParameters);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("OmadarxesTomea/{id}")]
    public async Task<ActionResult<OmadarxisDto>> GetOmadarxesAnaTomea([FromBody] TomeasDto tomea, OmadarxisQueryParameters queryParameters)
    {
        var result = await _stelexiService.GetOmadarxesSeTomeaInService(tomea, queryParameters);
        if (result is null)
            return NotFound();

        return Ok(result);
    }


    [HttpPut("Omadarxi/{id}")]
    public async Task<IActionResult> PutOmadarxi(int id, [FromBody] OmadarxisDto omadarxisDto)
    {
        var result = await _stelexiService.UpdateStelexosInService(id , omadarxisDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("Omadarxi/{id}")]
    public async Task<IActionResult> DeleteOmadarxi(int id)
    {
        var result = await _stelexiService.DeleteStelexosByIdInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }
}

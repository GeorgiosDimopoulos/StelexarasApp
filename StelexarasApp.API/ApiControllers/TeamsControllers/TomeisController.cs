using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Library.Dtos.Domi;
using Microsoft.AspNetCore.Authorization;
using StelexarasApp.Library.QueryParameters;

namespace StelexarasApp.API.ApiControllers.TeamsControllers;

[ApiController]
[Route("[controller]")]
public class TomeisController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpGet("Tomeis")]
    public async Task<ActionResult<IEnumerable<TomeasDto>>> GetTomeis([FromQuery] TomeasQueryParameters queryParameters)
    {
        var result = await _teamsService.GetAllTomeisInService(queryParameters);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Tomea/{name}")]
    public async Task<ActionResult<TomeasDto>> GetTomea(string name, [FromQuery] TomeasQueryParameters queryParameters)
    {
        var result = await _teamsService.GetTomeaByNameInService(queryParameters, name);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [Authorize]
    [HttpPost("Tomea")]
    public async Task<ActionResult<TomeasDto>> PostTomea(TomeasDto tomeasDto)
    {
        var result = await _teamsService.AddTomeasInService(tomeasDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("Tomea/{id}")]
    public async Task<IActionResult> DeleteTomea(int id)
    {
        var result = await _teamsService.DeleteTomeasInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("Tomea/{id}")]
    public async Task<IActionResult> PutTomea(int id, [FromBody] TomeasDto tomeasDto)
    {
        var result = await _teamsService.UpdateTomeaInService(id, tomeasDto);

        if (!result)
            return StatusCode(500, "An error occurred while updating the Tomea.");
        return Ok(result);
    }
}

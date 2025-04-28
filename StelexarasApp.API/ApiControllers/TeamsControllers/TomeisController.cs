using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<ActionResult<TomeasDto>> PostTomea([FromQuery]TomeasDto tomeasDto)
    {
        var result = await _teamsService.AddTomeasInService(tomeasDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("Tomea/{name}")]
    public async Task<IActionResult> DeleteTomea(string name)
    {
        var result = await _teamsService.DeleteTomeasInService(name);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPut("Tomea/{name}")]
    public async Task<IActionResult> PutTomea(string name, [FromBody] TomeasDto tomeasDto)
    {
        var result = await _teamsService.UpdateTomeaInService(name, tomeasDto);

        if (!result)
            return StatusCode(500, "An error occurred while updating the Tomea.");
        return Ok(result);
    }
}

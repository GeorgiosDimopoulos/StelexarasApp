using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.QueryParameters.Domi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.API.Controllers.TeamsControllers;

[ApiController]
[Route("[controller]")]
public class TomeisController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpGet("Tomeis")]
    public async Task<ActionResult<IEnumerable<TomeasResponse>>> GetTomeis([FromQuery] TomeasQueryParameters queryParameters)
    {
        var result = await _teamsService.GetAllTomeisInService(queryParameters);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Tomea/{name}")]
    public async Task<ActionResult<TomeasResponse>> GetTomea(string name, [FromQuery] TomeasQueryParameters queryParameters)
    {
        var result = await _teamsService.GetTomeaByNameInService(queryParameters, name);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [Authorize]
    [HttpPost("Tomea")]
    public async Task<ActionResult<CreateTomeasRequest>> PostTomea([FromQuery] CreateTomeasRequest tomeasDto)
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
    public async Task<IActionResult> PutTomea(string name, [FromBody] UpdateTomeasRequest tomeasDto)
    {
        var result = await _teamsService.UpdateTomeaInService(name, tomeasDto);

        if (!result)
            return StatusCode(500, "An error occurred while updating the Tomea.");
        return Ok(result);
    }
}

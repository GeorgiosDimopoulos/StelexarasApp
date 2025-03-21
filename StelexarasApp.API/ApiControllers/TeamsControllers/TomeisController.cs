using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Library.Dtos.Domi;

namespace StelexarasApp.API.ApiControllers.TeamsControllers;

[ApiController]
[Route("[controller]")]
public class TomeisController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpPost("Tomea")]
    public async Task<ActionResult<TomeasDto>> PostTomea(TomeasDto tomeasDto)
    {
        var result = await _teamsService.AddTomeasInService(tomeasDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

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

    [HttpGet("Tomeis")]
    public async Task<ActionResult<IEnumerable<TomeasDto>>> GetTomeis()
    {
        var result = await _teamsService.GetAllTomeisInService(new());

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Tomea/{name}")]
    public async Task<ActionResult<TomeasDto>> GetTomea(string name)
    {
        var result = await _teamsService.GetTomeaByNameInService(new(), name);

        if (result is null)
            return NotFound();
        return Ok(result);
    }
}

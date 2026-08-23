using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.TeamsControllers;

[ApiController]
[Route("[controller]")]
public class TomeisController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    /// <remarks>
    /// To get the tomearxi,just IncludeStelexos
    /// </remarks>        
    [HttpGet("Tomeis")]
    public async Task<ActionResult<IEnumerable<TomeasResponse>>> GetTomeis([FromQuery] TomeasQueryParameters queryParameters)
    {
        var result = await _teamsService.GetAllTomeisInService(queryParameters);

        return Ok(result);
    }

    /// <remarks>
    /// To get the tomearxi,just IncludeStelexos
    /// </remarks>        
    [HttpGet("Tomea/{name}")]
    public async Task<ActionResult<TomeasResponse>> GetTomea(string name, [FromQuery] TomeasQueryParameters queryParameters)
    {
        var result = await _teamsService.GetTomeaByNameInService(queryParameters, name);

        if (result.IsFailed)
            return NotFound(result.Errors.Select(e => e.Message));

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost("Tomea")]
    public async Task<ActionResult> PostTomea([FromBody] CreateTomeasRequest tomeasDto)
    {
        var result = await _teamsService.AddTomeasInService(tomeasDto);

        if (!result.IsSuccess)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [Authorize]
    [HttpDelete("Tomea/{name}")]
    public async Task<ActionResult> DeleteTomea(string name)
    {
        var result = await _teamsService.DeleteTomeasInService(name);

        if (!result.IsSuccess)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [Authorize]
    [HttpPut("Tomea/{name}")]
    public async Task<ActionResult> PutTomea(string name, [FromQuery] UpdateTomeasRequest tomeasDto)
    {
        var result = await _teamsService.UpdateTomeaInService(name, tomeasDto);

        if (!result.IsSuccess)
            return BadRequest(result.Errors.Select(e => e.Message));
        return Ok();
    }
}

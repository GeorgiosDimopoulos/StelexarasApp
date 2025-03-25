using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.QueryParameters;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers.TeamsControllers;

[ApiController]
[Route("[controller]")]
public class SkinesController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpGet("Skines")]
    public async Task<ActionResult<IEnumerable<SkiniDto>>> GetSkines([FromQuery] SkiniQueryParameters skinQueryParameters)
    {
        var result = await _teamsService.GetAllSkinesInService(skinQueryParameters);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Skini/{name}")]
    public async Task<ActionResult<SkiniDto>> GetSkini([FromQuery] SkiniQueryParameters parameters, string name)
    {
        var result = await _teamsService.GetSkiniByNameInService(parameters, name);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("Skini")]
    public async Task<ActionResult<SkiniDto>> PostSkini([FromBody] SkiniDto skiniDto)
    {
        var result = await _teamsService.AddSkiniInService(skiniDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("Skini/{id}")]
    public async Task<IActionResult> PutSkini(int id, [FromBody] SkiniDto skiniDto)
    {
        var result = await _teamsService.UpdateSkiniInService(id, skiniDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("Skini/{id}")]
    public async Task<IActionResult> DeleteSkini(int id)
    {
        var result = await _teamsService.DeleteSkiniInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Skines/{koinotitaName}")]
    public async Task<ActionResult<IEnumerable<SkiniDto>>> GetSkinesByKoinotita(string koinotitaName)
    {
        var result = await _teamsService.GetSkinesAnaKoinotitaInService(new(), koinotitaName);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("SkinesEkpaideuomenon")]
    public async Task<ActionResult<IEnumerable<SkiniDto>>> GetSkinesEkpaideuomenon()
    {
        var result = await _teamsService.GetSkinesEkpaideuomenonInService(new());

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}

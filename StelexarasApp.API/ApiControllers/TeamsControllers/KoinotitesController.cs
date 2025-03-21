using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers.TeamsControllers;

[ApiController]
[Route("[controller]")]
public class KoinotitesController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpGet("Koinotites")]
    public async Task<ActionResult<IEnumerable<KoinotitaDto>>> GetKoinotites()
    {
        var result = await _teamsService.GetAllKoinotitesInService(new());

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Koinotites/{tomeaId}")]
    public async Task<ActionResult<IEnumerable<KoinotitaDto>>> GetKoinotitesByTomea(int tomeaId)
    {
        var result = await _teamsService.GetKoinotitesAnaTomeaInService(new(), tomeaId);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("Koinotita")]
    public async Task<ActionResult<KoinotitaDto>> PostKoinotita([FromBody] KoinotitaDto koinotitaDto)
    {
        var result = await _teamsService.AddKoinotitaInService(koinotitaDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("Koinotita/{id}")]
    public async Task<IActionResult> DeleteKoinotita(int id)
    {
        var result = await _teamsService.DeleteKoinotitaInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("Koinotita/{id}")]
    public async Task<IActionResult> PutKoinotita(int id, [FromBody] KoinotitaDto koinotitaDto)
    {
        var result = await _teamsService.UpdateKoinotitaInService(id, koinotitaDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Koinotita/{name}")]
    public async Task<ActionResult<KoinotitaDto>> GetKoinotita(string name)
    {
        var result = await _teamsService.GetKoinotitaByNameInService(new(), name);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}

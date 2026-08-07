using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.TeamsControllers;

[ApiController]
[Route("[controller]")]
public class KoinotitesController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    /// <remarks>
    /// To get the koinotarxes,just IncludeStelexos
    /// </remarks>
    [HttpGet("Koinotites")]
    public async Task<ActionResult<IEnumerable<KoinotitaResponse>>> GetKoinotites([FromQuery] KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var result = await _teamsService.GetAllKoinotitesInService(koinotitaQueryParameters);

        if (result is null)
            return NotFound();
        return Ok(result);
    }


    /// <remarks>
    /// To get the koinotarxi, just IncludeStelexos
    /// </remarks>
    [HttpGet("Koinotita/{name}")]
    public async Task<ActionResult<KoinotitaResponse>> GetKoinotitaByName(string name, [FromQuery] KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var result = await _teamsService.GetKoinotitaByNameInService(koinotitaQueryParameters, name);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <remarks>
    /// To get the koinotarxi, just IncludeStelexos
    /// </remarks>
    [HttpGet("Koinotita/{id:int}")]
    public async Task<ActionResult<KoinotitaResponse>> GetKoinotitaById(int id, [FromQuery] KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var result = await _teamsService.GetKoinotitaByIdInService(id, koinotitaQueryParameters);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <remarks>
    /// To get the koinotarxes,just IncludeStelexos
    /// </remarks>
    [AllowAnonymous]
    [HttpGet("Koinotites/{tomeaId}/Koinotites")]
    public async Task<ActionResult<IEnumerable<KoinotitaResponse>>> GetKoinotitesByTomea(int tomeaId, [FromQuery] KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var result = await _teamsService.GetKoinotitesAnaTomeaInService(koinotitaQueryParameters, tomeaId);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <remarks>
    /// For tomeasName, write only A or B
    /// </remarks>    
    /// <param name="koinotitaDto"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Koinotita")]
    public async Task<ActionResult<bool>> PostKoinotita([FromQuery] CreateKoinotitaRequest koinotitaDto)
    {
        var result = await _teamsService.AddKoinotitaInService(koinotitaDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("Koinotita/{id}")]
    public async Task<IActionResult> DeleteKoinotita(int id)
    {
        var result = await _teamsService.DeleteKoinotitaInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPut("Koinotita/{id}")]
    public async Task<IActionResult> PutKoinotita(int id, [FromBody] UpdateKoinotitaRequest koinotitaDto)
    {
        var result = await _teamsService.UpdateKoinotitaInService(id, koinotitaDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }
}

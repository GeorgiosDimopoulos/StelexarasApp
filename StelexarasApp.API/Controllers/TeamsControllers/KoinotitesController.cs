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

        return Ok(result);
    }

    /// <remarks>
    /// To get the koinotarxi, just IncludeStelexos
    /// </remarks>
    [HttpGet("Koinotita/ByName/{name}")]
    public async Task<ActionResult<KoinotitaResponse>> GetKoinotitaByName(string name, [FromQuery] KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var result = await _teamsService.GetKoinotitaByNameInService(koinotitaQueryParameters, name);

        if (result.IsFailed)
            return NotFound(result.Errors.First().Message);

        return Ok(result.Value);
    }

    /// <remarks>
    /// To get the koinotarxi, just IncludeStelexos
    /// </remarks>
    [HttpGet("Koinotita/ById/{id:int}")]
    public async Task<ActionResult<KoinotitaResponse>> GetKoinotitaById(int id, [FromQuery] KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var result = await _teamsService.GetKoinotitaByIdInService(id, koinotitaQueryParameters);

        if (result.IsFailed)
            return NotFound(result.Errors.First().Message);

        return Ok(result.Value);
    }

    /// <remarks>
    /// To get the koinotarxes,just IncludeStelexos
    /// </remarks>
    [AllowAnonymous]
    [HttpGet("Koinotites/{tomeaId}/Koinotites")]
    public async Task<ActionResult<IEnumerable<KoinotitaResponse>>> GetKoinotitesByTomea(int tomeaId, [FromQuery] KoinotitaQueryParameters koinotitaQueryParameters)
    {
        var result = await _teamsService.GetKoinotitesAnaTomeaInService(koinotitaQueryParameters, tomeaId);

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

        if (!result.IsSuccess)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [Authorize]
    [HttpDelete("Koinotita/{id}")]
    public async Task<ActionResult> DeleteKoinotita(int id)
    {
        var result = await _teamsService.DeleteKoinotitaInService(id);

        if (!result.IsSuccess)
            return NotFound(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [Authorize]
    [HttpPut("Koinotita/{id}")]
    public async Task<ActionResult> PutKoinotita(int id, [FromQuery] UpdateKoinotitaRequest koinotitaDto)
    {
        var result = await _teamsService.UpdateKoinotitaInService(id, koinotitaDto);

        if (!result.IsSuccess)
            return NotFound(result.Errors.Select(e => e.Message));

        return Ok();
    }
}

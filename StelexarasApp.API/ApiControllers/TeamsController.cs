using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.QueryParameters;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers;

[Authorize]
[ApiExplorerSettings(IgnoreApi = false)]
public class TeamsController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpGet("Skines")]
    public async Task<ActionResult<IEnumerable<SkiniDto>>> GetSkines(SkiniQueryParameters skinQueryParameters)
    {
        var result = await _teamsService.GetAllSkinesInService(skinQueryParameters);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Skini/{name}")]
    public async Task<ActionResult<SkiniDto>> GetSkini(SkiniQueryParameters parameters, string name)
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
    public async Task<IActionResult> PutSkini([FromBody] SkiniDto skiniDto)
    {
        var result = await _teamsService.UpdateSkiniInService(skiniDto);

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
    public async Task<IActionResult> PutKoinotita([FromBody] KoinotitaDto koinotitaDto)
    {
        var result = await _teamsService.UpdateKoinotitaInService(koinotitaDto);

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

    [HttpGet("SkinesEkpaideuomenon")]
    public async Task<ActionResult<IEnumerable<SkiniDto>>> GetSkinesEkpaideuomenon()
    {
        var result = await _teamsService.GetSkinesEkpaideuomenonInService(new());

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("Tomea")]
    public async Task<ActionResult<TomeasDto>> PostTomea(TomeasDto tomeasDto)
    {
        var result = await _teamsService.AddTomeasInService(tomeasDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("Tomea/{name}")]
    public async Task<IActionResult> DeleteTomea(string name)
    {
        var result = await _teamsService.DeleteTomeasInService(name);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("Tomea/{name}")]
    public async Task<IActionResult> PutTomea(string name, [FromBody] TomeasDto tomeasDto)
    {
        var tomea = await _teamsService.GetTomeaByNameInService(new(), name);
        if (tomea == null)
            return NotFound($"Tomea with name '{name}' not found.");

        tomea.Name = tomeasDto.Name;
        tomea.KoinotitesNumber = tomeasDto.KoinotitesNumber;

        var result = await _teamsService.UpdateTomeaInService(tomeasDto);

        if (!result)
            return StatusCode(500, "An error occurred while updating the Tomea.");
        return Ok(result);
    }
}
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.ApiControllers;

public class TeamsController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpGet("Skines")]
    public async Task<ActionResult<IEnumerable<SkiniDto>>> GetSkines()
    {
        var result = await _teamsService.GetAllSkinesInService();

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Skini/{name}")]
    public async Task<ActionResult<SkiniDto>> GetSkini(string name)
    {
        var result = await _teamsService.GetSkiniByNameInService(name);

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
        var result =await _teamsService.GetSkinesAnaKoinotitaInService(koinotitaName);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Koinotites")]
    public async Task<ActionResult<IEnumerable<KoinotitaDto>>> GetKoinotites()
    {
        var result = await _teamsService.GetAllKoinotitesInService();

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Koinotites/{tomeaId}")]
    public async Task<ActionResult<IEnumerable<KoinotitaDto>>> GetKoinotitesByTomea(int tomeaId)
    {
        var result = await _teamsService.GetKoinotitesAnaTomeaInService(tomeaId);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Tomeis")]
    public async Task<ActionResult<IEnumerable<TomeasDto>>> GetTomeis()
    {
        var result = await _teamsService.GetAllTomeisInService();

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Tomea/{name}")]
    public async Task<ActionResult<TomeasDto>> GetTomea(string name)
    {
        var result = await _teamsService.GetTomeaByNameInService(name);

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
        var result = await _teamsService.GetKoinotitaByNameInService(name);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("SkinesEkpaideuomenon")]
    public async Task<ActionResult<IEnumerable<SkiniDto>>> GetSkinesEkpaideuomenon()
    {
        var result = await _teamsService.GetSkinesEkpaideuomenonInService();

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("Tomea")]
    public async Task<ActionResult<TomeasDto>> PostTomea([FromBody] TomeasDto tomeasDto)
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
    public async Task<IActionResult> PutTomea([FromBody] TomeasDto tomeasDto)
    {
        var result = await _teamsService.UpdateTomeaInService(tomeasDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }
}
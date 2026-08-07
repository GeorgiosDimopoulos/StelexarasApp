using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.QueryParameters.Domi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.API.Controllers.TeamsControllers;

[ApiController]
[Route("[controller]")]
public class SkinesController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpGet("Skines")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkines([FromQuery] SkiniQueryParameters skinQueryParameters)
    {
        var result = await _teamsService.GetAllSkinesInService(skinQueryParameters);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Skini/{name}")]
    public async Task<ActionResult<SkiniResponse>> GetSkini([FromQuery] SkiniQueryParameters parameters, string name)
    {
        var result = await _teamsService.GetSkiniByNameInService(parameters, name);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPost("Skini")]
    public async Task<IActionResult> PostSkini([FromQuery] CreateSkiniRequest skiniDto)
    {
        var result = await _teamsService.AddSkiniInService(skiniDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPut("Skini/{id}")]
    public async Task<IActionResult> PutSkini(int id, [FromBody] UpdateSkiniRequest skiniDto)
    {
        var result = await _teamsService.UpdateSkiniInService(id, skiniDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("Skini/{id}")]
    public async Task<IActionResult> DeleteSkini(int id)
    {
        var result = await _teamsService.DeleteSkiniInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Skines/ByKoinotitaName/{koinotitaName}")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkinesByKoinotitaName(string koinotitaName)
    {
        var result = await _teamsService.GetSkinesAnaKoinotitaNameInService(new(), koinotitaName);

        if (result is null)
            return NotFound();
        return Ok(result);
    }


    [HttpGet("Skines/ByKoinotitaId/{koinotitaId:int}")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkinesByKoinotitaId(int koinotitaId)
    {
        var result = await _teamsService.GetSkinesAnaKoinotitaIdInService(new(), koinotitaId);

        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("SkinesEkpaideuomenon")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkinesEkpaideuomenon()
    {
        var result = await _teamsService.GetSkinesEkpaideuomenonInService(new());

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}

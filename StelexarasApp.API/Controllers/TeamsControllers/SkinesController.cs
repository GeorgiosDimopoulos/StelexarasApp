using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        return Ok(result);
    }

    [HttpGet("Skini/{id:int}")]
    public async Task<ActionResult<SkiniResponse>> GetSkiniById([FromQuery] SkiniQueryParameters parameters, int id)
    {
        var result = await _teamsService.GetSkiniByIdInService(parameters, id);

        if (result.IsFailed)
            return NotFound(result.Errors.Select(e => e.Message));

        return Ok(result.Value);
    }

    [HttpGet("Skini/{name}")]
    public async Task<ActionResult<SkiniResponse>> GetSkiniByName([FromQuery] SkiniQueryParameters parameters, string name)
    {
        var result = await _teamsService.GetSkiniByNameInService(parameters, name);

        if (result.IsFailed)
            return NotFound(result.Errors.Select(e => e.Message));

        return Ok(result.Value);
    }

    [HttpGet("Skines/ByKoinotitaName/{koinotitaName}")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkinesByKoinotitaName(string koinotitaName, [FromQuery] SkiniQueryParameters skiniQueryParameters)
    {
        var result = await _teamsService.GetSkinesAnaKoinotitaNameInService(skiniQueryParameters, koinotitaName);

        return Ok(result);
    }


    [HttpGet("Skines/ByKoinotitaId/{koinotitaId:int}")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkinesByKoinotitaId(int koinotitaId, [FromQuery] SkiniQueryParameters skiniQueryParameters)
    {
        var result = await _teamsService.GetSkinesAnaKoinotitaIdInService(skiniQueryParameters, koinotitaId);

        return Ok(result);
    }

    [HttpGet("SkinesEkpaideuomenon")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkinesEkpaideuomenon()
    {
        var result = await _teamsService.GetSkinesEkpaideuomenonInService(new());

        return Ok(result);
    }

    [Authorize]
    [HttpPost("Skini")]
    public async Task<ActionResult> PostSkini([FromBody] CreateSkiniRequest skiniDto)
    {
        var result = await _teamsService.AddSkiniInService(skiniDto);

        if (!result.IsSuccess)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [Authorize]
    [HttpPut("Skini/{id}")]
    public async Task<ActionResult> PutSkini(int id, [FromQuery] UpdateSkiniRequest skiniDto)
    {
        var result = await _teamsService.UpdateSkiniInService(id, skiniDto);

        if (!result.IsSuccess)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [Authorize]
    [HttpDelete("Skini/{id}")]
    public async Task<ActionResult> DeleteSkini(int id)
    {
        var result = await _teamsService.DeleteSkiniInService(id);

        if (!result.IsSuccess)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }
}

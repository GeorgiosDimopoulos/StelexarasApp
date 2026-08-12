using Microsoft.AspNetCore.Mvc;
using Refit;

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

    [HttpGet("Skini/{id:int}")]
    public async Task<ActionResult<SkiniResponse>> GetSkiniById([FromQuery] SkiniQueryParameters parameters, int id)
    {
        var result = await _teamsService.GetSkiniByIdInService(parameters, id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Skini/{name}")]
    public async Task<ActionResult<SkiniResponse>> GetSkiniByName([FromQuery] SkiniQueryParameters parameters, string name)
    {
        var result = await _teamsService.GetSkiniByNameInService(parameters, name);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("Skini")]
    public async Task<bool> PostSkini([Body] CreateSkiniRequest skiniDto)
    {
        var result = await _teamsService.AddSkiniInService(skiniDto);

        if (!result)
            return false;

        return true;
    }

    [HttpPut("Skini/{id}")]
    public async Task<bool> PutSkini(int id, [Body] UpdateSkiniRequest skiniDto)
    {
        var result = await _teamsService.UpdateSkiniInService(id, skiniDto);

        if (!result)
            return false;

        return true;
    }

    [HttpDelete("Skini/{id}")]
    public async Task<bool> DeleteSkini(int id)
    {
        var result = await _teamsService.DeleteSkiniInService(id);

        if (!result)
            return false;

        return true;
    }

    [HttpGet("Skines/ByKoinotitaName/{koinotitaName}")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkinesByKoinotitaName(string koinotitaName, [FromQuery] SkiniQueryParameters skiniQueryParameters)
    {
        var result = await _teamsService.GetSkinesAnaKoinotitaNameInService(skiniQueryParameters, koinotitaName);

        if (result is null)
            return NotFound();
        return Ok(result);
    }


    [HttpGet("Skines/ByKoinotitaId/{koinotitaId:int}")]
    public async Task<ActionResult<IEnumerable<SkiniResponse>>> GetSkinesByKoinotitaId(int koinotitaId, [FromQuery] SkiniQueryParameters skiniQueryParameters)
    {
        var result = await _teamsService.GetSkinesAnaKoinotitaIdInService(skiniQueryParameters, koinotitaId);

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

using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.TeamsControllers;


[ApiController]
[Route("[controller]")]
public class AnwtatoiXwroiController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    /// <remarks>
    /// To get the koinotarxes,just IncludeStelexos
    /// </remarks>
    [HttpGet("AnwatatoiXwroi")]
    public async Task<ActionResult<IEnumerable<string>>> GetAnwtatoiXwroi()
    {
        var result = await _teamsService.GetAnwtatoiXwroi();

        if (result is null)
            return NotFound();
        return Ok(result);
    }

}

using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.TeamsControllers;


[ApiController]
[Route("[controller]")]
public class AnwtatoiXwroiController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    /// <remarks>
    /// To get the anwtatoi/leaders,just IncludeStelexos
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetAnwtatoiXwroi()
    {
        var result = await _teamsService.GetAnwtatoiXwroi();
        return Ok(result ?? []);
    }
}

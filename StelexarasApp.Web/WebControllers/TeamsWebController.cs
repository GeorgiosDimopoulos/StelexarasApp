using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Web.WebControllers;

public class TeamsWebController (ITeamsService teamsService, ILogger<TeamsWebController> logger) : Controller
{
    private readonly ITeamsService _teamsService = teamsService;
    private readonly ILogger<TeamsWebController> _logger = logger;

    // GET: TeamsWeb
    public async Task<IActionResult> Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var allSkines = await _teamsService.GetAllSkines();
            var allKoinotites = await _teamsService.GetAllKoinotites();
            var allTomeis = await _teamsService.GetAllTomeis();
            var allTeams = allSkines.Concat(allKoinotites).Concat(allTomeis);

            if (allTeams == null || !allTeams.Any())
            {
                _logger.LogWarning("Not all Teams found.");
                return NotFound("Not all Teams Data");
            }
            return View(allTeams);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching in Index Teams.");
            return View("Error");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
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
            var allSkines = await _teamsService.GetAllSkinesInService();
            var allKoinotites = await _teamsService.GetAllKoinotitesInService();
            var allTomeis = await _teamsService.GetAllTomeisInService();
            var allTeams = allSkines.Cast<object>().Concat(allKoinotites).Concat(allTomeis);

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

    // GET: TeamsWeb/Details/5
    public async Task<IActionResult> Details(string name)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var skini = await _teamsService.GetSkiniByNameInService(name);
            if (skini == null)
            {
                _logger.LogWarning("Skini not found.");
                return NotFound("Skini not found.");
            }
            return View(skini);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching in Details Teams.");
            return View("Error");
        }
    }

    // POST: TeamsWeb/Create
    [HttpPost]
    public async Task<IActionResult> Create(SkiniDto skini)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _teamsService.AddSkiniInService(skini);
            if (!result)
            {
                _logger.LogWarning("Skini not created.");
                return NotFound("Skini not created.");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating Skini.");
            return View("Error");
        }
    }

    // GET: TeamsWeb/Edit/5
    public async Task<IActionResult> Edit(string name)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var skini = await _teamsService.GetSkiniByNameInService(name);
            if (skini == null)
            {
                _logger.LogWarning("Skini not found.");
                return NotFound("Skini not found.");
            }
            return View(skini);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching in Edit Teams.");
            return View("Error");
        }
    }

    // GET: TeamsWeb/Delete/5
    public async Task<IActionResult> Delete(string name)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var skini = await _teamsService.GetSkiniByNameInService(name);
            if (skini == null)
            {
                _logger.LogWarning("Skini not found.");
                return NotFound("Skini not found.");
            }
            return View(skini);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching in Delete Teams.");
            return View("Error");
        }
    }

    // POST: TeamsWeb/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _teamsService.DeleteSkiniInService(id);
            if (!result)
            {
                _logger.LogWarning("Skini not deleted.");
                return NotFound("Skini not deleted.");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting Skini.");
            return View("Error");
        }
    }
}

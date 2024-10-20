using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.WebControllers;

[Route("SkinesWeb")]
public class SkinesWebController (ITeamsService teamsService, ILogger<SkinesWebController> logger) : Controller
{
    private readonly ITeamsService _teamsService = teamsService;
    private readonly ILogger<SkinesWebController> _logger = logger;

    // GET: TeamsWeb
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var allSkines = await _teamsService.GetAllSkinesInService();
            var allKoinotites = await _teamsService.GetAllKoinotitesInService();
            var allTomeis = await _teamsService.GetAllTomeisInService();

            if ((allSkines == null || !allSkines.Any()) && (allKoinotites == null || !allKoinotites.Any()) && (allTomeis == null || !allTomeis.Any()))
            {
                _logger.LogWarning("Not all Teams found.");
                return NotFound("Not all Teams Data");
            }

            var allTeams = allSkines.Cast<object>().Concat(allKoinotites).Concat(allTomeis);
            return View(allTeams);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching in Index Teams.");
            return View(null);
        }
    }

    // GET: SkinesWeb/Create
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }


    // GET: TeamsWeb/Details/5
    [HttpPost("Details/{name}")]
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
    [ValidateAntiForgeryToken]
    [HttpPost("create")]
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
    [HttpGet("edit/{name}")]
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
    [HttpGet("delete/{name}")]
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
    [HttpPost("deleteconfirmed/{id}")]
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

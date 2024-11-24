using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.Controllers.WebControllers;

[Route("KoinotitaWeb")]
public class KoinotitaWebController : Controller
{
    private readonly ITeamsService _teamsService;
    private readonly ILogger<KoinotitaWebController> _logger;

    public KoinotitaWebController(ITeamsService teamsService, ILogger<KoinotitaWebController> logger)
    {
        _teamsService = teamsService ?? throw new ArgumentNullException(nameof(teamsService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var allKoinotites = await _teamsService.GetAllKoinotitesInService();
            if (allKoinotites == null || !allKoinotites.Any())
            {
                _logger.LogWarning("No Koinotites found.");
                return NotFound("No Koinotites Data");
            }

            return View(allKoinotites);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Koinotites.");
            return View("Error", new { errorMessage = ex.Message });
        }
    }

    // GET: Koinotita/Details/5
    [HttpGet("Details/{name}")]
    public async Task<IActionResult> Details(string name)
    {
        if (string.IsNullOrEmpty(name))
            return NotFound();

        try
        {
            var koinotita = await _teamsService.GetKoinotitaByNameInService(name);
            if (koinotita == null)
            {
                _logger.LogWarning("Koinotita not found.");
                return NotFound("Koinotita not found.");
            }

            return View(koinotita);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Koinotita details.");
            return View("Error");
        }
    }

    // POST: Koinotita/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KoinotitaDto koinotita)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _teamsService.AddKoinotitaInService(koinotita);
            if (!result)
            {
                _logger.LogWarning("Koinotita not created.");
                return NotFound("Koinotita not created.");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating Koinotita.");
            return View("Error");
        }
    }

    // GET: Koinotita/Edit/5
    [HttpGet("edit/{name}")]
    public async Task<IActionResult> Edit(string name)
    {
        if (string.IsNullOrEmpty(name))
            return NotFound();

        try
        {
            var koinotita = await _teamsService.GetKoinotitaByNameInService(name);
            if (koinotita == null)
            {
                _logger.LogWarning("Koinotita not found.");
                return NotFound("Koinotita not found.");
            }

            return View(koinotita);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Koinotita for editing.");
            return View("Error");
        }
    }

    // GET: Koinotita/Delete/5
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return NotFound();

        try
        {
            var koinotita = await _teamsService.GetKoinotitaByNameInService(id.ToString());
            if (koinotita == null)
            {
                _logger.LogWarning("Koinotita not found.");
                return NotFound("Koinotita not found.");
            }

            return View(koinotita);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Koinotita for deletion.");
            return View("Error");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.WebControllers;

[Route("KoinotitaWeb")]
public class KoinotitaWebController(ITeamsService teamsService, ILogger<KoinotitaWebController> logger) : Controller
{
    private readonly ITeamsService _teamsService = teamsService ?? throw new ArgumentNullException(nameof(teamsService));
    private readonly ILogger<KoinotitaWebController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        Console.WriteLine("Starting Index method in KoinotitaWebController!");

        try
        {            
            var allKoinotites = await teamsService.GetAllKoinotitesInService();
            if (allKoinotites == null || !allKoinotites.Any())
            {
                _logger.LogWarning("Not all Koinotites found.");
                return NotFound("Not all Koinotites Data");
            }

            return View(allKoinotites);
        }
        catch (Exception ex)
        {
            return View("Error", new { errorMessage = ex.Message });
        }
    }

    // GET: Koinotita/Details/5
    [HttpGet("Details/{name}")]
    public async Task<IActionResult> Details(string name)
    {
        if (name == null)
            return NotFound();

        var koinotita = await _teamsService.GetKoinotitaByNameInService(name);

        if (koinotita == null)
            return NotFound();

        return View(koinotita);
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
                _logger.LogWarning("koinotita not created.");
                return NotFound("koinotita not created.");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating Skini.");
            return View("Error");
        }
    }

    // GET: Koinotita/Edit/5
    [HttpGet("edit/{name}")]
    public async Task<IActionResult> Edit(string name)
    {
        if (name == null)
            return NotFound();

        var koinotita = await _teamsService.GetKoinotitaByNameInService(name);
        if (koinotita == null)
            return NotFound();

        // ViewData ["KoinotarxisId"] = new SelectList(koinotarxisList, "Id", "FullName", koinotita.KoinotarxisId);
        return View(koinotita);
    }

    // GET: Koinotita/Delete/5
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return NotFound();

        var koinotita = await _teamsService.DeleteKoinotitaInService(id);

        if (koinotita == false)
            return NotFound();
        return View(koinotita);
    }

    // POST: Koinotita/Delete/5
    [HttpPost("DeleteConfirmed/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var koinotita = await _teamsService.GetKoinotitaByNameInService(id);
        if (koinotita != null)
            await _teamsService.DeleteKoinotitaInService(koinotita.Id);

        return RedirectToAction(nameof(Index));
    }
}

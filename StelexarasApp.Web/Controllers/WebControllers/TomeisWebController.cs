using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.Controllers.WebControllers;

[Route("TomeisWeb")]
public class TomeisWebController : Controller
{
    private readonly ITeamsService _tomeisService;
    private readonly ILogger<TomeisWebController> _logger;

    public TomeisWebController(ITeamsService tomeisService, ILogger<TomeisWebController> logger)
    {
        _tomeisService = tomeisService;
        _logger = logger;
    }

    // GET: TomeisWeb
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var allTomeis = await _tomeisService.GetAllTomeisInService();
            if (allTomeis == null || !allTomeis.Any())
            {
                _logger.LogWarning("No Tomeis found.");
                return NotFound("No Tomeis Data");
            }
            return View(allTomeis);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Tomeis.");
            return View("Error");
        }
    }

    // GET: TomeisWeb/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(string id)
    {
        try
        {
            var tomeis = await _tomeisService.GetTomeaByNameInService(id);
            if (tomeis == null)
            {
                _logger.LogWarning("Tomeis not found.");
                return NotFound("Tomeis not found.");
            }
            return View(tomeis);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Tomeis details.");
            return View("Error");
        }
    }

    // POST: TomeisWeb/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TomeasDto tomeis)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _tomeisService.AddTomeasInService(tomeis);
            if (!result)
            {
                _logger.LogWarning("Tomeis not created.");
                return NotFound("Tomeis not created.");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating Tomeis.");
            return View("Error");
        }
    }

    // GET: TomeisWeb/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
        try
        {
            var tomeis = await _tomeisService.GetTomeaByNameInService(id);
            if (tomeis == null)
            {
                _logger.LogWarning("Tomeis not found.");
                return NotFound("Tomeis not found.");
            }
            return View(tomeis);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Tomeis for editing.");
            return View("Error");
        }
    }

    // POST: TomeisWeb/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, TomeasDto tomeis)
    {
        if (id != tomeis.Name)
            return BadRequest("Tomeis ID mismatch.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _tomeisService.UpdateTomeaInService(tomeis);
            if (!result)
            {
                _logger.LogWarning("Tomeis not updated.");
                return NotFound("Tomeis not updated.");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating Tomeis.");
            return View("Error");
        }
    }

    // GET: TomeisWeb/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var tomeis = await _tomeisService.GetTomeaByNameInService(id);
            if (tomeis == null)
            {
                _logger.LogWarning("Tomeis not found.");
                return NotFound("Tomeis not found.");
            }
            return View(tomeis);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Tomeis for deletion.");
            return View("Error");
        }
    }

    // POST: TomeisWeb/Delete/5
    [HttpPost("DeleteConfirmed/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        try
        {
            var result = await _tomeisService.DeleteTomeasInService(id);
            if (!result)
            {
                _logger.LogWarning("Tomeis not deleted.");
                return NotFound("Tomeis not deleted.");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting Tomeis.");
            return View("Error");
        }
    }
}

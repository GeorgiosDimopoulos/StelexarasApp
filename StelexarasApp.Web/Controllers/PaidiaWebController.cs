using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.Library.Dtos.People.Children;
using StelexarasApp.Library.Models.Atoma.Children;
using StelexarasApp.Services.Interfaces.People;
using StelexarasApp.Services.IServices;
using System.Collections;

namespace StelexarasApp.Web.Controllers.WebControllers;

[Route("PaidiaWeb")]
public class PaidiaWebController : Controller
{
    private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiaService;
    private readonly ITeamsService _teamsService;
    private readonly ILogger<PaidiaWebController> _logger;

    public PaidiaWebController(IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> paidiaService, ITeamsService teamsService, ILogger<PaidiaWebController> logger)
    {
        _paidiaService = paidiaService;
        _teamsService = teamsService;
        _logger = logger;
    }

    // GET: PaidiaWeb
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var paidia = await _paidiaService.GetPaidiaInService(PaidiType.Kataskinotis);
            if (paidia == null || !paidia.Any())
            {
                _logger.LogWarning("No Paidia found for the specified type.");
                return NotFound("No Paidia Data");
            }
            return View(paidia);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching in Index Paidia.");
            return View("Error");
        }
    }

    // GET: PaidiaWeb/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        try
        {
            ViewData ["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkinesInService(new()), "Id", "Name");
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Create Paidi.");
            return View("Error");
        }
    }

    // POST: PaidiaWeb/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,Age,Sex,PaidiType,SkiniName")] CreatePaidiRequest paidi)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _paidiaService.CreatePaidiInService(paidi);

                if (result)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "An error occurred while adding the Paidi.");
            }

            //ViewData["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkinesInService(new()), "Id", "Name", paidi.SkiniId);
            return View(paidi);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Create Paidi.");
            return View("Error");
        }
    }

    // GET: PaidiaWeb/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            if (id <= 0)
                return NotFound();

            var paidi = await _paidiaService.GetPaidiByIdInService(id);
            if (paidi == null)
                return NotFound();

            //ViewData["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkinesInService(new()), "Id", "Name", paidi.SkiniId);
            return View(paidi);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Edit Paidi.");
            return View("Error");
        }
    }

    // POST: PaidiaWeb/Edit/5
    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FullName,Id,Age,Sex,PaidiType,SkiniName")] UpdatePaidiRequest paidi)
    {
        if (id != paidi.Id || paidi is null)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _paidiaService.UpdatePaidiInService(paidi);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existed = await _paidiaService.GetPaidiByIdInService(paidi.Id);
                if (existed is null)
                    return NotFound("DbUpdateConcurrencyException: Paidi not found in Db");
                else
                    return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        //ViewData["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkinesInService(new()), "Id", "Name", paidi.SkiniId);
        return View(paidi);
    }

    // GET: PaidiaWeb/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (id <= 0)
                return NotFound();

            var paidi = await _paidiaService.GetPaidiByIdInService(id);
            if (paidi == null)
                return NotFound();

            return View(paidi);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Delete Paidi.");
            return View("Error");
        }
    }
}

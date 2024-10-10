using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services.IServices;
using System.Collections;

namespace StelexarasApp.Web.WebControllers;

public class StaffWebController(IStaffService staffService, ITeamsService teamsService, ILogger<StaffWebController> logger) : Controller
{
    private readonly IStaffService _staffService = staffService;
    private readonly ILogger<StaffWebController> _logger = logger;
    private readonly ITeamsService _teamsService = teamsService;

    // GET: StaffWeb
    public async Task<IActionResult> Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var staff = await _staffService.GetAllStaffInService();
            if (staff == null || !staff.Any())
            {
                _logger.LogWarning("No Staff found.");
                return NotFound("No Staff Data");
            }
            return View(staff);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching in Index Staff.");
            return View("Error");
        }
    }

    // GET: StaffWeb/Details/5
    public async Task<IActionResult> Details(int id, Thesi thesi)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var staff = await _staffService.GetStelexosByIdInService(id, thesi);
            if (staff == null)
            {
                _logger.LogWarning("No Staff found.");
                return NotFound("No Staff Data");
            }
            return View(staff);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching in Details Staff.");
            return View("Error");
        }
    }

    // POST: StaffWeb/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,Age,Sex,XwrosName")] IStelexosDto stelexos, Thesi thesi)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _staffService.AddStelexosInService(stelexos);
                if (result)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "An error occurred while adding the Stelexos.");
            }

            ViewData ["SkiniId"] = new SelectList(await _teamsService.GetAllSkinesInService(), "Id", "Name", stelexos.DtoXwrosName);
            return View(stelexos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Create Stelexos.");
            return View("Error");
        }
    }

    // GET: StaffWeb/Edit/5
    public async Task<IActionResult> Edit(int id, Thesi thesi)
    {
        try
        {
            if (id <= 0)
                return NotFound();

            var paidi = await _staffService.GetStelexosByIdInService(id, thesi);
            if (paidi == null)
                return NotFound();

            ViewData ["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkinesInService(), "Id", "Name", paidi.DtoXwrosName);
            return View(paidi);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Edit Stelexos.");
            return View("Error");
        }
    }

    // POST: StaffWeb/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FullName,Id,Age,Sex,Thesi,SkiniName")] IStelexosDto stelexosDto, Thesi thesi)
    {
        if (id != stelexosDto.Id || stelexosDto is null)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _staffService.UpdateStelexosInService(stelexosDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existed = _staffService.GetStelexosByIdInService(id, thesi);
                if (existed is null)
                    return NotFound("DbUpdateConcurrencyException: Stelexos not found in Db");
                else
                    return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData ["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkinesInService(), "Id", "Name", stelexosDto.DtoXwrosName);
        return View(stelexosDto);
    }

    // GET: StaffWeb/Delete/5
    public async Task<IActionResult> Delete(int id, Thesi thesi)
    {
        try
        {
            if (id <= 0)
                return NotFound();

            var paidi = await _staffService.DeleteStelexosByIdInService(id, thesi);
            if (paidi == false)
                return NotFound();

            return View(paidi);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Delete Stelexos.");
            return View("Error");
        }
    }

    // POST: StaffWeb/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, Thesi thesi)
    {
        try
        {
            var result = await _staffService.DeleteStelexosByIdInService(id, thesi);
            if (result == false)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "An error occurred while deleting the Stelexos.");
            return View("Error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Delete Stelexos.");
            return View("Error");
        }
    }
}

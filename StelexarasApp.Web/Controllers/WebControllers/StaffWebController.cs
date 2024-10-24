﻿using Microsoft.AspNetCore.Mvc;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.Controllers.WebControllers;

[Route("StaffWeb")]
public class StaffWebController : Controller
{
    private readonly IStaffService _staffService;
    private readonly ILogger<StaffWebController> _logger;

    public StaffWebController(IStaffService staffService, ILogger<StaffWebController> logger)
    {
        _staffService = staffService;
        _logger = logger;
    }

    // GET: StaffWeb
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var staffList = await _staffService.GetAllStaffInService();
            if (staffList == null || !staffList.Any())
            {
                _logger.LogWarning("No staff members found.");
                return NotFound("No staff data available.");
            }
            return View(staffList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the staff list.");
            ViewData["ErrorMessage"] = "An error occurred while fetching the staff list. Please try again later.";
            return View("Error");
        }
    }

    // GET: StaffWeb/Details/5
    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id, [FromQuery] Thesi? thesi)
    {
        if (id <= 0)
            return BadRequest("Invalid staff ID.");

        try
        {
            var staffMember = await _staffService.GetStelexosByIdInService(id, thesi);
            if (staffMember == null)
            {
                _logger.LogWarning($"Staff member with ID {id} not found.");
                return NotFound("Staff member not found.");
            }
            return View(staffMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching details for staff member with ID {id}.");
            return View("Error", new { message = $"An error occurred while fetching details for staff member with ID {id}." });
        }
    }

    // GET: StaffWeb/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: StaffWeb/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,Position,Phone")] IStelexosDto staffDto)
    {
        if (!ModelState.IsValid)
            return View(staffDto);

        try
        {
            var result = await _staffService.AddStelexosInService(staffDto);
            if (result)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to create staff member.");
            return View(staffDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new staff member.");
            return View("Error", new { message = "An error occurred while creating a new staff member." });
        }
    }

    // GET: StaffWeb/Edit/5
    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id, [FromQuery] Thesi? thesi)
    {
        if (id <= 0)
            return BadRequest("Invalid staff ID.");

        try
        {
            var staffMember = await _staffService.GetStelexosByIdInService(id, thesi);
            if (staffMember == null)
            {
                _logger.LogWarning($"Staff member with ID {id} not found.");
                return NotFound("Staff member not found.");
            }
            return View(staffMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching staff member with ID {id} for editing.");
            return View("Error", new { message = $"An error occurred while fetching staff member with ID {id} for editing." });
        }
    }

    // GET: StaffWeb/Delete/5
    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] Thesi? thesi)
    {
        if (id <= 0)
            return BadRequest("Invalid staff ID.");

        try
        {
            var staffMember = await _staffService.GetStelexosByIdInService(id, thesi);
            if (staffMember == null)
            {
                _logger.LogWarning($"Staff member with ID {id} not found.");
                return NotFound("Staff member not found.");
            }
            return View(staffMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching staff member with ID {id} for deletion.");
            return View("Error", new { message = $"An error occurred while fetching staff member with ID {id} for deletion." });
        }
    }

    // POST: StaffWeb/Delete/5
    [HttpPost("Delete/{id:int}"), ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, [FromQuery] Thesi thesi)
    {
        try
        {
            var result = await _staffService.DeleteStelexosByIdInService(id, thesi);
            if (!result)
            {
                ModelState.AddModelError("", "Failed to delete staff member.");
                return View("Error", new { message = "Failed to delete staff member." });
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting staff member with ID {id}.");
            return View("Error", new { message = $"An error occurred while deleting staff member with ID {id}." });
        }
    }
}

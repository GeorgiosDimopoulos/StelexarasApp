﻿using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Models;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.Controllers.WebControllers;

[Route("DutiesWeb")]
public class DutiesWebController : Controller
{
    private readonly IDutyService _dutyService;

    public DutiesWebController(IDutyService dutyService)
    {
        _dutyService = dutyService;
    }

    // GET: DutiesWeb
    [HttpGet] // Explicitly specify that this action is a GET request
    public async Task<IActionResult> Index()
    {
        try
        {
            var duties = await _dutyService.GetDutiesInService();
            if (duties == null)
                return NotFound();

            return View(duties);
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    // GET: DutiesWeb/Create
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: DutiesWeb/Create
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Date")] Duty duty)
    {
        if (ModelState.IsValid)
        {
            await _dutyService.AddDutyInService(duty);
            return RedirectToAction(nameof(Index));
        }

        return View(duty);
    }

    // GET: DutiesWeb/Edit/5
    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0)
            return NotFound();

        var duty = await _dutyService.GetDutyByIdInService(id);
        if (duty == null)
            return NotFound();

        return View(duty);
    }

    // POST: DutiesWeb/Edit/5
    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date")] Duty duty)
    {
        if (ModelState.IsValid)
        {
            var existingDuty = await _dutyService.GetDutyByIdInService(id);
            if (existingDuty == null)
            {
                return NotFound($"Duty with name {id} not found.");
            }

            duty.Id = existingDuty.Id;
            await _dutyService.UpdateDutyInService(duty.Name, duty);
            return RedirectToAction(nameof(Index));
        }

        return View(duty);
    }

    // GET: DutiesWeb/Delete/5    
    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return NotFound();

        var duty = await _dutyService.GetDutyByIdInService(id);
        if (duty == null)
            return NotFound();

        return View(duty);
    }

    private bool DutyExists(int id)
    {
        return _dutyService.GetDutyByIdInService(id) != null;
    }
}

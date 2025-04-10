﻿using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.Controllers.WebControllers;

[Route("KoinotitesWeb")]
public class KoinotitesWebController(ITeamsService koinotitaService, ILogger<KoinotitesWebController> logger) : Controller
{
    private readonly ITeamsService _koinotitaService = koinotitaService;
    private readonly ILogger<KoinotitesWebController> _logger = logger;

    // GET: KoinotitaWeb
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var allKoinotites = await _koinotitaService.GetAllKoinotitesInService(new());
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
            return View("Error");
        }
    }

    // POST: KoinotitaWeb/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KoinotitaDto koinotita)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _koinotitaService.AddKoinotitaInService(koinotita);
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

    // GET: KoinotitaWeb/Edit/5
    [HttpGet("edit/{name}")]
    public async Task<IActionResult> Edit(string name)
    {
        try
        {
            var koinotita = await _koinotitaService.GetKoinotitaByNameInService(new(),name);
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

    // GET: KoinotitaWeb/Delete/5
    [HttpGet("delete/{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        try
        {
            var koinotita = await _koinotitaService.GetKoinotitaByNameInService(null, name);
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

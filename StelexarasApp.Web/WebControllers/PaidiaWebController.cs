using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.IServices;
using System.Collections;

namespace StelexarasApp.Web.WebControllers
{
    public class PaidiaWebController(IPaidiaService paidiaService, ITeamsService teamsService, ILogger<PaidiaWebController> logger) : Controller
    {
        private readonly IPaidiaService _paidiaService = paidiaService;
        private readonly ITeamsService _teamsService = teamsService;
        private readonly ILogger<PaidiaWebController> _logger = logger;

        // GET: PaidiaWeb
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

        // GET: PaidiaWeb/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound("");

                var paidi = await _paidiaService.GetPaidiByIdInService(id);

                if (paidi == null)
                    return NotFound();
                return View(paidi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Details of Paidia.");
                return View("Error");
            }
        }

        // GET: PaidiaWeb/Create
        public IActionResult Create()
        {
            try
            {
                ViewData ["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkines(), "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Create Paidi.");
                return View("Error");
            }
        }

        // POST: PaidiaWeb/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Age,Sex,PaidiType,SkiniName")] PaidiDto paidi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _paidiaService.AddPaidiInService(paidi);

                    if (result)
                        return View(paidi); //  return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", "An error occurred while adding the Paidi.");
                }

                ViewData ["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkines(), "Id", "Name", paidi.SkiniId);
                return View(paidi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Create Paidi.");
                return View("Error");
            }
        }

        // GET: PaidiaWeb/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();

                var paidi = await _paidiaService.GetPaidiByIdInService(id);
                if (paidi == null)
                    return NotFound();

                ViewData ["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkines(), "Id", "Name", paidi.SkiniId);
                return View(paidi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Edit Paidi.");
                return View("Error");
            }
        }

        // POST: PaidiaWeb/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FullName,Id,Age,Sex,PaidiType,SkiniName")] PaidiDto paidi)
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
                    var existed = _paidiaService.GetPaidiByIdInService(paidi.Id);
                    if (existed is null)
                        return NotFound("DbUpdateConcurrencyException: Paidi not found in Db");
                    else
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData ["SkiniId"] = new SelectList((IEnumerable)_teamsService.GetAllSkines(), "Id", "Name", paidi.SkiniId);
            return View(paidi);
        }

        // GET: PaidiaWeb/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();

                var paidi = await _paidiaService.DeletePaidiInService(id);
                if (paidi == false)
                    return NotFound();

                return View(paidi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Delete Paidi.");
                return View("Error");
            }
        }

        // POST: PaidiaWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _paidiaService.DeletePaidiInService(id);
                if (result == false)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "An error occurred while deleting the Paidi.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Delete Paidi.");
                return View("Error");
            }
        }
    }
}

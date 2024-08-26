using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Web.WebControllers
{
    public class KoinotitaWebController : Controller
    {
        private readonly AppDbContext _context;

        public KoinotitaWebController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Koinotita
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Koinotites.Include(k => k.Koinotarxis);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Koinotita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koinotita = await _context.Koinotites
                .Include(k => k.Koinotarxis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (koinotita == null)
            {
                return NotFound();
            }

            return View(koinotita);
        }

        // GET: Koinotita/Create
        public IActionResult Create()
        {
            ViewData["KoinotarxisId"] = new SelectList(_context.Koinotarxes, "Id", "FullName");
            return View();
        }

        // POST: Koinotita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,KoinotarxisId")] Koinotita koinotita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(koinotita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KoinotarxisId"] = new SelectList(_context.Koinotarxes, "Id", "FullName", koinotita.KoinotarxisId);
            return View(koinotita);
        }

        // GET: Koinotita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koinotita = await _context.Koinotites.FindAsync(id);
            if (koinotita == null)
            {
                return NotFound();
            }
            ViewData["KoinotarxisId"] = new SelectList(_context.Koinotarxes, "Id", "FullName", koinotita.KoinotarxisId);
            return View(koinotita);
        }

        // POST: Koinotita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,KoinotarxisId")] Koinotita koinotita)
        {
            if (id != koinotita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(koinotita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KoinotitaExists(koinotita.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KoinotarxisId"] = new SelectList(_context.Koinotarxes, "Id", "FullName", koinotita.KoinotarxisId);
            return View(koinotita);
        }

        // GET: Koinotita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koinotita = await _context.Koinotites
                .Include(k => k.Koinotarxis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (koinotita == null)
            {
                return NotFound();
            }

            return View(koinotita);
        }

        // POST: Koinotita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var koinotita = await _context.Koinotites.FindAsync(id);
            if (koinotita != null)
            {
                _context.Koinotites.Remove(koinotita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KoinotitaExists(int id)
        {
            return _context.Koinotites.Any(e => e.Id == id);
        }
    }
}

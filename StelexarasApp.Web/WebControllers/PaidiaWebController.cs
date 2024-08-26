using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Web.WebControllers
{
    public class PaidiaWebController : Controller
    {
        private readonly AppDbContext _context;

        public PaidiaWebController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PaidiaWeb
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Paidia.Include(p => p.Skini);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PaidiaWeb/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paidi = await _context.Paidia
                .Include(p => p.Skini)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paidi == null)
            {
                return NotFound();
            }

            return View(paidi);
        }

        // GET: PaidiaWeb/Create
        public IActionResult Create()
        {
            ViewData["SkiniId"] = new SelectList(_context.Skines, "Id", "Name");
            return View();
        }

        // POST: PaidiaWeb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Id,Age,SeAdeia,Sex,PaidiType,SkiniId")] Paidi paidi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paidi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkiniId"] = new SelectList(_context.Skines, "Id", "Name", paidi.SkiniId);
            return View(paidi);
        }

        // GET: PaidiaWeb/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paidi = await _context.Paidia.FindAsync(id);
            if (paidi == null)
            {
                return NotFound();
            }
            ViewData["SkiniId"] = new SelectList(_context.Skines, "Id", "Name", paidi.SkiniId);
            return View(paidi);
        }

        // POST: PaidiaWeb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FullName,Id,Age,SeAdeia,Sex,PaidiType,SkiniId")] Paidi paidi)
        {
            if (id != paidi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paidi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaidiExists(paidi.Id))
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
            ViewData["SkiniId"] = new SelectList(_context.Skines, "Id", "Name", paidi.SkiniId);
            return View(paidi);
        }

        // GET: PaidiaWeb/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paidi = await _context.Paidia
                .Include(p => p.Skini)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paidi == null)
            {
                return NotFound();
            }

            return View(paidi);
        }

        // POST: PaidiaWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paidi = await _context.Paidia.FindAsync(id);
            if (paidi != null)
            {
                _context.Paidia.Remove(paidi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaidiExists(int id)
        {
            return _context.Paidia.Any(e => e.Id == id);
        }
    }
}

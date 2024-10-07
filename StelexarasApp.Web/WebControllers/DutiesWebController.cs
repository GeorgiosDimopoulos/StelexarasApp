using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Web.WebControllers;

public class DutiesWebController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;

    // GET: DutiesWeb
    public async Task<IActionResult> Index()
    {
        return View(await _context.Duties.ToListAsync());
    }

    // GET: DutiesWeb/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var duty = await _context.Duties
            .FirstOrDefaultAsync(m => m.Id == id);
        if (duty == null)
        {
            return NotFound();
        }

        return View(duty);
    }

    // GET: DutiesWeb/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DutiesWeb/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Date")] Duty duty)
    {
        if (ModelState.IsValid)
        {
            _context.Add(duty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(duty);
    }

    // GET: DutiesWeb/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var duty = await _context.Duties.FindAsync(id);
        if (duty == null)
        {
            return NotFound();
        }
        return View(duty);
    }

    // POST: DutiesWeb/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date")] Duty duty)
    {
        if (id != duty.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(duty);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DutyExists(duty.Id))
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
        return View(duty);
    }

    // GET: DutiesWeb/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var duty = await _context.Duties
            .FirstOrDefaultAsync(m => m.Id == id);
        if (duty == null)
        {
            return NotFound();
        }

        return View(duty);
    }

    // POST: DutiesWeb/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var duty = await _context.Duties.FindAsync(id);
        if (duty != null)
        {
            _context.Duties.Remove(duty);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DutyExists(int id)
    {
        return _context.Duties.Any(e => e.Id == id);
    }
}

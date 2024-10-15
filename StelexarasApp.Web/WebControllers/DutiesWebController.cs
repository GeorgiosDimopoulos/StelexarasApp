using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Web.WebControllers;

[Route("DutiesWeb")]
public class DutiesWebController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;

    // GET: DutiesWeb
    [HttpGet] // Explicitly specify that this action is a GET request
    public async Task<IActionResult> Index()
    {
        return View(await _context.Duties.ToListAsync());
    }

    // GET: DutiesWeb/Details/5
    [HttpGet("{id:int}")] // Explicit GET method with id parameter
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
            _context.Add(duty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(duty);
    }

    // GET: DutiesWeb/Edit/5
    [HttpGet("edit/{id:int}")]
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
    [HttpPost("edit/{id:int}")]
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
    [HttpGet("delete/{id:int}")] // Explicit GET method for delete confirmation
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
    [HttpPost("delete/{id:int}"), ActionName("DeleteConfirmed")] // Specify this as a POST request for delete confirmation
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

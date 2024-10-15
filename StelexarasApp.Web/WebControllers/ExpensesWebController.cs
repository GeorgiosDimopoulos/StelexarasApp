using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Web.WebControllers;

[Route("ExpensesWeb")]
public class ExpensesWebController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;

    // GET: ExpensesWeb
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Expenses.ToListAsync());
    }

    // GET: ExpensesWeb/Details/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var expense = await _context.Expenses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (expense == null)
            return NotFound();

        return View(expense);
    }

    // GET: ExpensesWeb/Create
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: ExpensesWeb/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Amount,Description,Date")] Expense expense)
    {
        if (ModelState.IsValid)
        {
            _context.Add(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(expense);
    }

    // GET: ExpensesWeb/Edit/5
    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
            return NotFound();

        return View(expense);
    }

    // POST: ExpensesWeb/Edit/5
    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Description,Date")] Expense expense)
    {
        if (id != expense.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(expense);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var expenseExists = _context.Expenses.Any(e => e.Id == id);
                if (!expenseExists)
                    return NotFound();
                else
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        return View(expense);
    }

    // GET: ExpensesWeb/Delete/5
    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var expense = await _context.Expenses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (expense == null)
            return NotFound();

        return View(expense);
    }

    // POST: ExpensesWeb/Delete/5
    [HttpPost("delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense != null)
            _context.Expenses.Remove(expense);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.Library.Models;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.Controllers.WebControllers;

[Route("ExpensesWeb")]
public class ExpensesWebController(IExpenseService expenseService) : Controller
{
    private readonly IExpenseService _expenseService = expenseService;

    // GET: ExpensesWeb
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var expenses = await _expenseService.GetExpensesInService();
            if (expenses == null || !expenses.Any())
                return NotFound("No Expenses Data");

            return View(expenses);
        }
        catch (Exception ex)
        {
            return View("Error");
        }
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
            await _expenseService.AddExpenseInService(expense);
            return RedirectToAction(nameof(Index));
        }
        return View(expense);
    }

    // GET: ExpensesWeb/Edit/5
    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0)
            return NotFound();

        var expense = await _expenseService.GetExpenseByIdInService(id);

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
                var updateResult = await _expenseService.UpdateExpenseInService(id, expense);
                if (updateResult)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Unable to update expense. Please try again.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _expenseService.HasData())
                    return NotFound();
                else
                    throw;
            }
        }
        return View(expense);
    }

    // GET: ExpensesWeb/Delete/5
    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var expense = await _expenseService.GetExpenseByIdInService(id.Value);
        if (expense == null)
            return NotFound();

        return View(expense);
    }
}

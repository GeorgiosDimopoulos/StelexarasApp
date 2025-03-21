using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.Library.Models;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var expenses = await _expenseService.GetExpensesInService();
        if (expenses == null)
            return NotFound();
        return Ok(expenses);
    }

    [AllowAnonymous]
    [HttpGet("Expense/{id}")]
    public async Task<ActionResult<Expense>> GetExpense(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var expense = await _expenseService.GetExpenseByIdInService(id);
        if (expense == null)
            return NotFound();
        return Ok(expense);
    }

    [HttpPost("Expense")]
    public async Task<ActionResult<Expense>> PostExpense(Expense expense)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _expenseService.AddExpenseInService(expense);

        if (result)
            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        return BadRequest();
    }

    [HttpPut("Expense/{id}")]
    public async Task<IActionResult> PutExpense(int id, Expense expense)
    {
        if (id != expense.Id)
            return BadRequest();

        try
        {
            await _expenseService.UpdateExpenseInService(id, expense);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _expenseService.HasData())
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("Expense/{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _expenseService.DeleteExpenseInService(id);
        if (!result)
            return NotFound();
        return Ok(result);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.ApiControllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class DutiesController : ControllerBase
    {
        private readonly IDutyService _dutyService;

        public DutiesController(IDutyService dutyService)
        {
            _dutyService = dutyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expenses = await _dutyService.GetDutiesInService();
            if (expenses == null)
                return NotFound();
            return Ok(expenses);
        }

        [HttpGet("Duty/{id}")]
        public async Task<ActionResult<Duty>> GetExpense(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expense = await _dutyService.GetDutyByIdInService(id);
            if (expense == null) 
                return NotFound();
            return Ok(expense);
        }

        [HttpPost("Duty")]
        public async Task<ActionResult<Expense>> PostExpense(Duty duty)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var expense = await _dutyService.AddDutyInService(duty);
            
            if (expense == false)
                return NotFound();
            return Ok(expense);
        }

        [HttpPut("Duty/{id}")]
        public async Task<IActionResult> PutExpense(int id, Duty duty)
        {
            if (id != duty.Id) 
                return BadRequest();
            
            var isUpdated = await _dutyService.UpdateDutyInService(duty.Name, duty);
            if (isUpdated == false)
                return NotFound();
            return Ok(isUpdated);
        }

        [HttpDelete("Duty/{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var isDeleted = await _dutyService.DeleteDutyInService(id);
            if (isDeleted == false)
                return NotFound();
            return Ok(isDeleted);
        }
    }
}

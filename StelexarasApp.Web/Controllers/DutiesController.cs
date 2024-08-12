using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Web.Controllers
{
    public class DutiesController : ControllerBase
    {
        private AppDbContext _context;

        public DutiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Duty>>> GetDuties()
        {
            return await _context.Duties.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Duty>> GetDuty(int id)
        {
            var duty = await _context.Duties.FindAsync(id);
            if (duty == null) return NotFound();
            return duty;
        }

        [HttpPost]
        public async Task<ActionResult<Duty>> PostDuty(Duty duty)
        {
            _context.Duties.Add(duty);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDuty), new { id = duty.Id }, duty);
        }

        [HttpPut]
        public async Task<IActionResult> PutDuty(int id, Duty duty)
        {
            if (id != duty.Id) return BadRequest();
            _context.Entry(duty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDuty(int id)
        {
            var duty = await _context.Duties.FindAsync(id);
            if (duty == null) return NotFound();
            _context.Duties.Remove(duty);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

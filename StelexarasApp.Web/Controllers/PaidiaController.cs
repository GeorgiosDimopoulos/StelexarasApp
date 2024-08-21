using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaidiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaidiaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paidi>>> GetPaidi()
        {
            return await _context.Paidia.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paidi>> GetPaidi(int id)
        {
            var paidi = await _context.Paidia.FindAsync(id);
            if (paidi == null) return NotFound();
            return paidi;
        }

        [HttpPost]
        public async Task<ActionResult<Paidi>> PostPaidi(Paidi paidi)
        {
            _context.Paidia.Add(paidi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPaidi), new { id = paidi.Id }, paidi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaidi(int id, Paidi paidi)
        {
            if (id != paidi.Id) return BadRequest();
            _context.Entry(paidi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaidi(int id)
        {
            var paidi = await _context.Paidia.FindAsync(id);
            if (paidi == null) return NotFound();
            _context.Paidia.Remove(paidi);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

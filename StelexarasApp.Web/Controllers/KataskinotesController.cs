using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;

namespace StelexarasApp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KataskinotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KataskinotesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kataskinotis>>> GetEkpaideuomenoi()
        {
            return await _context.Kataskinotes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kataskinotis>> GetKataskinotis(int id)
        {
            var kataskinotis = await _context.Kataskinotes.FindAsync(id);
            if (kataskinotis == null) return NotFound();
            return kataskinotis;
        }

        [HttpPost]
        public async Task<ActionResult<Kataskinotis>> PostKataskinotis(Kataskinotis kataskinotis)
        {
            var kat = await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetKataskinotis), new { id = kataskinotis.Id }, kataskinotis);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKataskinotis(int id, Kataskinotis kataskinotis)
        {
            if (id != kataskinotis.Id) return BadRequest();
            _context.Entry(kataskinotis).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEkpaideuomenos(int id)
        {
            var ekpaideuomenos = await _context.Ekpaideuomenoi.FindAsync(id);
            if (ekpaideuomenos == null) return NotFound();
            _context.Ekpaideuomenoi.Remove(ekpaideuomenos);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;

namespace StelexarasApp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EkpaideuomenoiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EkpaideuomenoiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ekpaideuomenos>>> GetEkpaideuomenoi()
        {
            return await _context.Ekpaideuomenoi.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ekpaideuomenos>> GetEkpaideuomenos(int id)
        {
            var ekpaideuomenos = await _context.Ekpaideuomenoi.FindAsync(id);
            if (ekpaideuomenos == null) return NotFound();
            return ekpaideuomenos;
        }

        [HttpPost]
        public async Task<ActionResult<Ekpaideuomenos>> PostEkpaideuomenos(Ekpaideuomenos ekpaideuomenos)
        {
            _context.Ekpaideuomenoi.Add(ekpaideuomenos);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEkpaideuomenos), new { id = ekpaideuomenos.Id }, ekpaideuomenos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEkpaideuomenos(int id, Ekpaideuomenos ekpaideuomenos)
        {
            if (id != ekpaideuomenos.Id) return BadRequest();
            _context.Entry(ekpaideuomenos).State = EntityState.Modified;
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

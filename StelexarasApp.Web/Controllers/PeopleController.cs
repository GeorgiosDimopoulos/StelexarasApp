using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;

namespace StelexarasApp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
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

        [HttpGet("Omadarxes")]
        public async Task<ActionResult<IEnumerable<Omadarxis>>> GetOmadarxes()
        {
            return await _context.Omadarxes.ToListAsync();
        }

        [HttpGet("Omadarxes/{id}")]
        public async Task<ActionResult<Omadarxis>> GetOmadarxis(int id)
        {
            var omadarxis = await _context.Omadarxes.FindAsync(id);
            if (omadarxis == null) return NotFound();
            return omadarxis;
        }

        [HttpPost("Omadarxi")]
        public async Task<ActionResult<Omadarxis>> PostOmadarxi(Omadarxis omadarxi)
        {
            _context.Omadarxes.Add(omadarxi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOmadarxis), new { id = omadarxi.Id }, omadarxi);
        }

        [HttpPut("Omadarxi/{id}")]
        public async Task<IActionResult> PutOmadarxi(int id, Omadarxis omadarxi)
        {
            if (id != omadarxi.Id) return BadRequest();
            _context.Entry(omadarxi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Omadarxi/{id}")]
        public async Task<IActionResult> DeleteOmadarxi(int id)
        {
            var omadarxi = await _context.Omadarxes.FindAsync(id);
            if (omadarxi == null) return NotFound();
            _context.Omadarxes.Remove(omadarxi);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("Koinotarxes")]
        public async Task<ActionResult<IEnumerable<Koinotarxis>>> GetKoinotarxes()
        {
            return await _context.Koinotarxes.ToListAsync();
        }

        [HttpGet("Koinotarxes/{id}")]
        public async Task<ActionResult<Koinotarxis>> GetKoinotarxis(int id)
        {
            var koinotarxis = await _context.Koinotarxes.FindAsync(id);
            if (koinotarxis == null) return NotFound();
            return koinotarxis;
        }

        [HttpPost("Koinotarxi")]
        public async Task<ActionResult<Koinotarxis>> PostKoinotarxi(Koinotarxis koinotarxi)
        {
            _context.Koinotarxes.Add(koinotarxi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetKoinotarxis), new { id = koinotarxi.Id }, koinotarxi);
        }

        [HttpPut("Koinotarxi/{id}")]
        public async Task<IActionResult> PutKoinotarxi(int id, Koinotarxis koinotarxi)
        {
            if (id != koinotarxi.Id) return BadRequest();
            _context.Entry(koinotarxi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Koinotarxi/{id}")]
        public async Task<IActionResult> DeleteKoinotarxi(int id)
        {
            var koinotarxi = await _context.Koinotarxes.FindAsync(id);
            if (koinotarxi == null) return NotFound();
            _context.Koinotarxes.Remove(koinotarxi);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("Tomearxes")]
        public async Task<ActionResult<IEnumerable<Tomearxis>>> GetTomearxes()
        {
            return await _context.Tomearxes.ToListAsync();
        }

        [HttpGet("Tomearxes/{id}")]
        public async Task<ActionResult<Tomearxis>> GetTomearxis(int id)
        {
            var tomearxis = await _context.Tomearxes.FindAsync(id);
            if (tomearxis == null) return NotFound();
            return tomearxis;
        }

        [HttpPost("Tomearxi")]
        public async Task<ActionResult<Tomearxis>> PostTomearxi(Tomearxis tomearxi)
        {
            _context.Tomearxes.Add(tomearxi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTomearxis), new { id = tomearxi.Id }, tomearxi);
        }

        [HttpPut("Tomearxi/{id}")]
        public async Task<IActionResult> PutTomearxi(int id, Tomearxis tomearxi)
        {
            if (id != tomearxi.Id) return BadRequest();
            _context.Entry(tomearxi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Tomearxi/{id}")]
        public async Task<IActionResult> DeleteTomearxi(int id)
        {
            var tomearxi = await _context.Tomearxes.FindAsync(id);
            if (tomearxi == null) return NotFound();
            _context.Tomearxes.Remove(tomearxi);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

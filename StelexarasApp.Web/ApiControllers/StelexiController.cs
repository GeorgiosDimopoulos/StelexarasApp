using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.IServices;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.Services.DtosModels.Atoma;

namespace StelexarasApp.Web.ApiControllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class StelexiController : ControllerBase
    {
        private readonly IStelexiService _stelexiService;

        public StelexiController(IStelexiService stelexiService)
        {
            _stelexiService = stelexiService;
        }

        [HttpGet("Omadarxes")]
        public async Task<ActionResult<IEnumerable<Stelexos>>> GetOmadarxes()
        {
            var result = await _stelexiService.GetStelexoiAnaThesi(Thesi.Omadarxis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Omadarxi/{id}")]
        public async Task<ActionResult<Omadarxis>> GetOmadarxis(int id)
        {
            var result = await _stelexiService.GetStelexosById(id, Thesi.Omadarxis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Omadarxi")]
        public async Task<ActionResult<Omadarxis>> PostOmadarxi([FromBody] OmadarxisDto omadarxisDto)
        {
            var result = await _stelexiService.AddStelexosInDbAsync(omadarxisDto, Thesi.Omadarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Omadarxi/{id}")]
        public async Task<IActionResult> PutOmadarxi(int id, [FromBody] OmadarxisDto omadarxisDto)
        {
            var result = await _stelexiService.UpdateStelexosInDb(id, omadarxisDto, Thesi.Omadarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("Omadarxi/{id}")]
        public async Task<IActionResult> DeleteOmadarxi(int id)
        {
            var result = await _stelexiService.DeleteStelexosInDb(id, Thesi.Omadarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Koinotarxes")]
        public async Task<ActionResult<IEnumerable<KoinotarxisDto>>> GetKoinotarxes()
        {
            var result = await _stelexiService.GetStelexoiAnaThesi(Thesi.Koinotarxis);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Koinotarxi/{id}")]
        public async Task<ActionResult<Koinotarxis>> GetKoinotarxis(int id)
        {
            var result = await _stelexiService.GetStelexosById(id, Thesi.Koinotarxis);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Koinotarxi")]
        public async Task<ActionResult<Koinotarxis>> PostKoinotarxi([FromBody] KoinotarxisDto koinotarxisDto)
        {
            var result = await _stelexiService.AddStelexosInDbAsync(koinotarxisDto, Thesi.Koinotarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Koinotarxi/{id}")]
        public async Task<IActionResult> PutKoinotarxi(int id, [FromBody] KoinotarxisDto koinotarxisDto)
        {
            var result = await _stelexiService.UpdateStelexosInDb(id, koinotarxisDto, Thesi.Koinotarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("Koinotarxi/{id}")]
        public async Task<IActionResult> DeleteKoinotarxi(int id)
        {
            var result = await _stelexiService.DeleteStelexosInDb(id, Thesi.Koinotarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Tomearxes")]
        public async Task<ActionResult<IEnumerable<Tomearxis>>> GetTomearxes()
        {
            var result = await _stelexiService.GetStelexoiAnaThesi(Thesi.Tomearxis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Tomearxi/{id}")]
        public async Task<ActionResult<Tomearxis>> GetTomearxis(int id)
        {
            var result = await _stelexiService.GetStelexosById(id, Thesi.Tomearxis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Tomearxi")]
        public async Task<ActionResult<Tomearxis>> PostTomearxi([FromBody] TomearxisDto tomearxisDto)
        {
            var result = await _stelexiService.AddStelexosInDbAsync(tomearxisDto, Thesi.Tomearxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Tomearxi/{id}")]
        public async Task<IActionResult> PutTomearxi(int id, [FromBody] TomearxisDto tomearxisDto)
        {
            var result = await _stelexiService.UpdateStelexosInDb(id, tomearxisDto, Thesi.Tomearxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("Tomearxi/{id}")]
        public async Task<IActionResult> DeleteTomearxi(int id)
        {
            var result = await _stelexiService.DeleteStelexosInDb(id, Thesi.Tomearxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Ekpaideutes")]
        public async Task<ActionResult<IEnumerable<Ekpaideutis>>> GetEkpaideutes()
        {
            var result = await _stelexiService.GetStelexoiAnaThesi(Thesi.Ekpaideutis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Ekpaideutis/{id}")]
        public async Task<ActionResult<Ekpaideutis>> GetEkpaideutis(int id)
        {
            var result = await _stelexiService.GetStelexosById(id, Thesi.Ekpaideutis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Ekpaideutis")]
        public async Task<ActionResult<Ekpaideutis>> PostEkpaideutis([FromBody] EkpaideutisDto ekpaideutisDto)
        {
            var result = await _stelexiService.AddStelexosInDbAsync(ekpaideutisDto, Thesi.Ekpaideutis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Ekpaideutis/{id}")]
        public async Task<IActionResult> PutEkpaideutis(int id, [FromBody] EkpaideutisDto ekpaideutisDto)
        {
            var result = await _stelexiService.UpdateStelexosInDb(id, ekpaideutisDto, Thesi.Ekpaideutis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("Ekpaideutis/{id}")]
        public async Task<IActionResult> DeleteEkpaideutis(int id)
        {
            var result = await _stelexiService.DeleteStelexosInDb(id, Thesi.Ekpaideutis);

            if (!result)
                return NotFound();

            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.ApiControllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _stelexiService;

        public StaffController(IStaffService stelexiService)
        {
            _stelexiService = stelexiService;
        }

        [HttpGet("Omadarxes")]
        public async Task<ActionResult<IEnumerable<Stelexos>>> GetOmadarxes()
        {
            var result = await _stelexiService.GetAllOmadarxesInService();

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Omadarxi/{id}")]
        public async Task<ActionResult<Omadarxis>> GetOmadarxis(int id)
        {
            var result = await _stelexiService.GetStelexosByIdInService(id, Thesi.Omadarxis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Omadarxi")]
        public async Task<ActionResult<Omadarxis>> PostOmadarxi([FromBody] OmadarxisDto omadarxisDto)
        {
            var result = await _stelexiService.AddStelexosInService(omadarxisDto, Thesi.Omadarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Omadarxi/{id}")]
        public async Task<IActionResult> PutOmadarxi([FromBody] OmadarxisDto omadarxisDto)
        {
            var result = await _stelexiService.UpdateStelexosInService(omadarxisDto);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("Omadarxi/{id}")]
        public async Task<IActionResult> DeleteOmadarxi(int id)
        {
            var result = await _stelexiService.DeleteStelexosByIdInService(id, Thesi.Omadarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Koinotarxes")]
        public async Task<ActionResult<IEnumerable<KoinotarxisDto>>> GetKoinotarxes()
        {
            var result = await _stelexiService.GetAllKoinotarxesInService();
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Koinotarxi/{id}")]
        public async Task<ActionResult<Koinotarxis>> GetKoinotarxis(int id)
        {
            var result = await _stelexiService.GetStelexosByIdInService(id, Thesi.Koinotarxis);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("OmadarxesKoinotitas/{id}")]
        public async Task<ActionResult<OmadarxisDto>> GetOmadarxesAnaKoinotita([FromBody] KoinotitaDto koinotita)
        {
            var result = await _stelexiService.GetOmadarxesSeKoinotitaInService(koinotita);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("OmadarxesTomea/{id}")]
        public async Task<ActionResult<OmadarxisDto>> GetOmadarxesAnaTomea([FromBody] TomeasDto tomea)
        {
            var result = await _stelexiService.GetOmadarxesSeTomeaInService(tomea);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("KoinotarxesTomea/{id}")]
        public async Task<ActionResult<OmadarxisDto>> GetKoinotarxesAnaTomea([FromBody] TomeasDto tomea)
        {
            var result = await _stelexiService.GetKoinotarxesSeTomeaInService(tomea);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Koinotarxi")]
        public async Task<ActionResult<Koinotarxis>> PostKoinotarxi([FromBody] KoinotarxisDto koinotarxisDto)
        {
            var result = await _stelexiService.AddStelexosInService(koinotarxisDto, Thesi.Koinotarxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Koinotarxi/{id}")]
        public async Task<IActionResult> PutKoinotarxi([FromBody] KoinotarxisDto koinotarxisDto)
        {
            var result = await _stelexiService.UpdateStelexosInService(koinotarxisDto);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("Koinotarxi/{id}")]
        public async Task<IActionResult> DeleteKoinotarxi(int id)
        {
            var result = await _stelexiService.DeleteStelexosByIdInService(id, Thesi.Koinotarxis);

            if (!result)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("StelexosByName")]
        public async Task<ActionResult<StelexosDto>> GetStelexosByNameInService(string name, string? thesiStr)
        {
            var thesi = Thesi.None;
            if (thesiStr is not null)
            {
                thesi = thesiStr switch
                {
                    "Omadarxis" => Thesi.Omadarxis,
                    "Koinotarxis" => Thesi.Koinotarxis,
                    "Tomearxis" => Thesi.Tomearxis,
                    "Ekpaideutis" => Thesi.Ekpaideutis,
                    _ => Thesi.None
                };
            }

            var result = await _stelexiService.GetStelexosByNameInService(name, thesi);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("Tomearxes")]
        public async Task<ActionResult<IEnumerable<Tomearxis>>> GetTomearxes()
        {
            var result = await _stelexiService.GetAllTomearxesInService();

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Tomearxi/{id}")]
        public async Task<ActionResult<Tomearxis>> GetTomearxis(int id)
        {
            var result = await _stelexiService.GetStelexosByIdInService(id, Thesi.Tomearxis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Tomearxi")]
        public async Task<ActionResult<Tomearxis>> PostTomearxi([FromBody] TomearxisDto tomearxisDto)
        {
            var result = await _stelexiService.AddStelexosInService(tomearxisDto, Thesi.Tomearxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Tomearxi/{id}")]
        public async Task<IActionResult> PutTomearxi([FromBody] TomearxisDto tomearxisDto)
        {
            var result = await _stelexiService.UpdateStelexosInService(tomearxisDto);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("Tomearxi/{id}")]
        public async Task<IActionResult> DeleteTomearxi(int id)
        {
            var result = await _stelexiService.DeleteStelexosByIdInService(id, Thesi.Tomearxis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Ekpaideutis/{id}")]
        public async Task<ActionResult<Ekpaideutis>> GetEkpaideutis(int id)
        {
            var result = await _stelexiService.GetStelexosByIdInService(id, Thesi.Ekpaideutis);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("Ekpaideutis")]
        public async Task<ActionResult<Ekpaideutis>> PostEkpaideutis([FromBody] EkpaideutisDto ekpaideutisDto)
        {
            var result = await _stelexiService.AddStelexosInService(ekpaideutisDto, Thesi.Ekpaideutis);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Ekpaideutis/{id}")]
        public async Task<IActionResult> PutEkpaideutis([FromBody] EkpaideutisDto ekpaideutisDto)
        {
            var result = await _stelexiService.UpdateStelexosInService(ekpaideutisDto);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("Ekpaideutis/{id}")]
        public async Task<IActionResult> DeleteEkpaideutis(int id)
        {
            var result = await _stelexiService.DeleteStelexosByIdInService(id, Thesi.Ekpaideutis);

            if (!result)
                return NotFound();

            return Ok(result);
        }
    }
}

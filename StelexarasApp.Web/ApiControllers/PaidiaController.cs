using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.IServices;
namespace StelexarasApp.Web.ApiControllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class PaidiaController : ControllerBase
    {
        private readonly IPaidiaService _paidiaService;

        public PaidiaController(IPaidiaService teamsService)
        {
            _paidiaService = teamsService;
        }

        [HttpGet("Paidia")]
        public async Task<ActionResult<IEnumerable<Paidi>>> GetPaidia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paidia = await _paidiaService.GetPaidia(PaidiType.Kataskinotis);
            return Ok(paidia);
        }

        [HttpGet("Ekpaideuomenoi")]
        public async Task<ActionResult<IEnumerable<Paidi>>> GetEkpaideuomenoi()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paidia = await _paidiaService.GetPaidia(PaidiType.Kataskinotis);
            return Ok(paidia);
        }

        [HttpGet("Paidi/{id}")]
        public async Task<ActionResult<Paidi>> GetPaidi(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paidi = await _paidiaService.GetPaidiById(id);
            if (paidi == null)
                return NotFound();

            return paidi;
        }

        [HttpPost("Paidi")]
        public async Task<ActionResult<Paidi>> PostPaidi([FromBody] PaidiDto paidiDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _paidiaService.AddPaidiInDbAsync(paidiDto);

            if (result)
            {
                return Ok(result);
            }

            return NotFound(); // or BadRequest?
        }

        [HttpPut("Paidi/{id}")]
        public async Task<IActionResult> PutPaidi(int id, PaidiDto paidiDto)
        {
            paidiDto.Id = id;
            var result = await _paidiaService.UpdatePaidiInDb(paidiDto);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaidi(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _paidiaService.DeletePaidiInDb(id);

            if (!result)
                return NotFound();

            return Ok(result);
        }
    }
}

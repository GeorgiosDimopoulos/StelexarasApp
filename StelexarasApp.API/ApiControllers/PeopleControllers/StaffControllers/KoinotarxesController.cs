using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.ApiControllers.PeopleControllers.StaffControllers;

[ApiController]
[Route("[controller]")]
public class KoinotarxesController(IKoinotarxisService koinotarxisService) : ControllerBase
{
    private readonly IKoinotarxisService _koinotarxisService = koinotarxisService
        ?? throw new ArgumentNullException(nameof(koinotarxisService));

    [HttpGet("Koinotarxes")]
    public async Task<ActionResult<IEnumerable<KoinotarxisDtoBase>>> GetKoinotarxes([FromQuery] KoinotarxisQueryParameters queryParameters)
    {
        var result = await _koinotarxisService.GetAllKoinotarxesInService(queryParameters);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Koinotarxi/{id}")]
    public async Task<ActionResult<Koinotarxis>> GetKoinotarxis(int id, KoinotarxisQueryParameters koinotarxisQueryParameters)
    {
        var result = await _koinotarxisService.GetKoinotarxisByIdInService(id, koinotarxisQueryParameters);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("KoinotarxesTomea/{name}")]
    public async Task<ActionResult<OmadarxisDtoBase>> GetKoinotarxesAnaTomea(string name, [FromQuery] KoinotarxisQueryParameters queryParameters)
    {
        var result = await _koinotarxisService.GetKoinotarxesSeTomeaInService(name, queryParameters);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPost("Koinotarxi")]
    public async Task<ActionResult<Koinotarxis>> PostKoinotarxi([FromBody] CreateKoinotarxisRequest koinotarxis)
    {
        if (koinotarxis == null)
        {
            return BadRequest("Koinotarxis parameters cannot be null");
        }

        var result = await _koinotarxisService.CreateKoinotarxisInService(koinotarxis);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPut("Koinotarxi/{id}")]
    public async Task<IActionResult> PutKoinotarxi([FromBody] UpdateKoinotarxisRequest koinotarxisDto, int id)
    {
        if (koinotarxisDto == null)
        {
            return BadRequest("Koinotarxis cannot be null");
        }

        var result = await _koinotarxisService.UpdateKoinotarxisInService(id, koinotarxisDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("Koinotarxi/{id}")]
    public async Task<IActionResult> DeleteKoinotarxi(int id)
    {
        var result = await koinotarxisService.DeleteKoinotarxisByIdInService(id);

        if (!result)
            return NotFound();
        return Ok(result);
    }
}

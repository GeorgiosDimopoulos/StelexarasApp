using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.API.ApiControllers.PeopleControllers.StaffControllers;

[ApiController]
[Route("api/[controller]")]
public class EkpaideutesController(IStaffService stelexiService) : ControllerBase
{
    private readonly IStaffService _stelexiService = stelexiService;

    [HttpGet("Ekpaideutes")]
    public async Task<ActionResult<Ekpaideutis>> GetEkpaideutis()
    {
        var result = await _stelexiService.GetAllEkpaideutesInService();

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Ekpaideutis/{id}")]
    public async Task<ActionResult<Ekpaideutis>> GetEkpaideutis(int id)
    {
        var result = await _stelexiService.GetStelexosByIdInService(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPost("Ekpaideutis")]
    public async Task<ActionResult<Ekpaideutis>> PostEkpaideutis([FromBody] EkpaideutisDto ekpaideutisDto)
    {
        var result = await _stelexiService.AddStelexosInService(ekpaideutisDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPut("Ekpaideutis/{id}")]
    public async Task<IActionResult> PutEkpaideutis(int id, [FromBody] EkpaideutisDto ekpaideutisDto)
    {
        var result = await _stelexiService.UpdateStelexosInService(id, ekpaideutisDto);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("Ekpaideutis/{id}")]
    public async Task<IActionResult> DeleteEkpaideutis(int id)
    {
        var result = await _stelexiService.DeleteStelexosByIdInService(id);

        if (!result)
            return NotFound();

        return Ok(result);
    }
}

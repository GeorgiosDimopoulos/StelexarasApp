using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class StelexiController(IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> stelexiService) : ControllerBase
{
    private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> _stelexiService = stelexiService;

    [HttpGet()]
    public async Task<ActionResult<StelexosResponse>> GetStelexi(Thesi? thesi,[FromQuery] StelexosQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexi(thesi, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);    
    }

    [HttpGet("StelexiAnaXwro")]
    public async Task<ActionResult<StelexosResponse>> GetStelexiByXwro(string xwrosName, [FromQuery] StelexosQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexoiAnaXwroInDb(xwrosName, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("StelexosByName")]
    public async Task<ActionResult<StelexosResponse>> GetStelexosByName(string name, [FromQuery] StelexosQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexosByName(name, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("Stelexos/{id:int}")]
    public async Task<ActionResult<StelexosResponse>> GetStelexosById(int id, [FromQuery] StelexosQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexosById(id, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("Stelexos")]
    public async Task<ActionResult<bool>> PostStelexos([FromBody] CreateStelexosRequest request)
    {
        var result = await _stelexiService.CreateStelexos(request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut("Stelexos")]
    public async Task<ActionResult<bool>> UpdateStelexos(int id, [FromBody] UpdateStelexosRequest request)
    {
        var result = await _stelexiService.UpdateStelexos(id, request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpDelete("Stelexos")]
    public async Task<ActionResult<bool>> DeleteStelexos([FromQuery]int id)
    {
        var result = await _stelexiService.DeleteStelexos(id);
        if (!result)
            return BadRequest();
        return Ok(result);
    }
}
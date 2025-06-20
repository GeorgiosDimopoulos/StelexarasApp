using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class StaffController(IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse> stelexiService) : ControllerBase
{
    private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse> _stelexiService = stelexiService;

    [HttpGet("Omadarxes")]
    public async Task<ActionResult<StelexosDtoBase>> GetOmadarxes(string name, OmadarxisQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexi(Thesi.Omadarxis, name, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("OmadarxisByName")]
    public async Task<ActionResult<StelexosDtoBase>> GetOmadarxisByName(string name)
    {
        var result = await _stelexiService.GetStelexosByName(Thesi.Omadarxis, name, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("OmadarxisById")]
    public async Task<ActionResult<StelexosDtoBase>> GetOmadarxisById(int id)
    {
        var result = await _stelexiService.GetStelexosById(id, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("Omadarxis")]
    public async Task<ActionResult<bool>> CreateOmadarxis([FromBody] CreateStelexosRequest request)
    {
        var result = await _stelexiService.CreateStelexos(request, Thesi.Omadarxis);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut("Omadarxis")]
    public async Task<ActionResult<bool>> UpdateOmadarxis(int id, [FromBody] UpdateStelexosRequest request)
    {
        var result = await _stelexiService.UpdateStelexos(id, request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpDelete("Omadarxis")]
    public async Task<ActionResult<bool>> DeleteOmadarxis([FromBody] DeleteStelexosRequest request)
    {
        var result = await _stelexiService.DeleteStelexos(request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpGet("Koinotarxes")]
    public async Task<ActionResult<StelexosDtoBase>> GetKoinotarxes(string name, KoinotarxisQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexi(Thesi.Koinotarxis, name, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("KoinotarxisByName")]
    public async Task<ActionResult<StelexosDtoBase>> GetKoinotarxisByName(string name)
    {
        var result = await _stelexiService.GetStelexosByName(Thesi.Koinotarxis, name, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("KoinotarxisById")]
    public async Task<ActionResult<StelexosDtoBase>> GetKoinotarxisById(int id)
    {
        var result = await _stelexiService.GetStelexosById(id, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("Koinotarxis")]
    public async Task<ActionResult<bool>> CreateKoinotarxis([FromBody] CreateStelexosRequest request)
    {
        var result = await _stelexiService.CreateStelexos(request, Thesi.Koinotarxis);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut("Koinotarxis")]
    public async Task<ActionResult<bool>> UpdateKoinotarxis(int id, [FromBody] UpdateStelexosRequest request)
    {
        var result = await _stelexiService.UpdateStelexos(id, request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpDelete("Koinotarxis")]
    public async Task<ActionResult<bool>> DeleteKoinotarxis([FromBody] DeleteStelexosRequest request)
    {
        var result = await _stelexiService.DeleteStelexos(request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpGet("Tomearxes")]
    public async Task<ActionResult<StelexosDtoBase>> GetTomearxes(TomearxisQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexi(Thesi.Tomearxis, string.Empty, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("TomearxisByName")]
    public async Task<ActionResult<StelexosDtoBase>> GetTomearxisByName(string name, TomearxisQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexosByName(Thesi.Tomearxis, name, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("TomearxisById")]
    public async Task<ActionResult<StelexosDtoBase>> GetTomearxisById(int id)
    {
        var result = await _stelexiService.GetStelexosById(id, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("Tomearxis")]
    public async Task<ActionResult<bool>> CreateTomearxis([FromBody] CreateStelexosRequest request)
    {
        var result = await _stelexiService.CreateStelexos(request, Thesi.Tomearxis);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut("Tomearxis")]
    public async Task<ActionResult<bool>> UpdateTomearxis(int id, [FromBody] UpdateStelexosRequest request)
    {
        var result = await _stelexiService.UpdateStelexos(id, request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpDelete("Tomearxis")]
    public async Task<ActionResult<bool>> DeleteTomearxis([FromBody] DeleteStelexosRequest request)
    {
        var result = await _stelexiService.DeleteStelexos(request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpGet("Ekpaideutis")]
    public async Task<ActionResult<StelexosDtoBase>> GetEkpaideutis(string name)
    {
        var result = await _stelexiService.GetStelexi(Thesi.Ekpaideutis, name, null!);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("EkpaideutisByName")]
    public async Task<ActionResult<StelexosDtoBase>> GetEkpaideutisByName(string name)
    {
        var result = await _stelexiService.GetStelexosByName(Thesi.Ekpaideutis, name, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("EkpaideutisById")]
    public async Task<ActionResult<StelexosDtoBase>> GetEkpaideutisById(int id)
    {
        var result = await _stelexiService.GetStelexosById(id, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("Ekpaideutis")]
    public async Task<ActionResult<bool>> CreateEkpaideutis([FromBody] CreateStelexosRequest request)
    {
        var result = await _stelexiService.CreateStelexos(request, Thesi.Ekpaideutis);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut("Ekpaideutis")]
    public async Task<ActionResult<bool>> UpdateEkpaideutis(int id, [FromBody] UpdateStelexosRequest request)
    {
        var result = await _stelexiService.UpdateStelexos(id, request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    [HttpDelete("Ekpaideutis")]
    public async Task<ActionResult<bool>> DeleteEkpaideutis([FromBody] DeleteStelexosRequest request)
    {
        var result = await _stelexiService.DeleteStelexos(request);
        if (!result)
            return BadRequest();
        return Ok(result);
    }
}
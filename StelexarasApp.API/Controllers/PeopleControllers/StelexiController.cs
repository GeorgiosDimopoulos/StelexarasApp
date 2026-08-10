using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class StelexiController(IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> stelexiService) : ControllerBase
{
    private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> _stelexiService = stelexiService;

    [HttpGet("Stelexi")]
    public async Task<ActionResult<StelexosDtoBase>> GetStelexi(string name, StelexosQueryParameters stelexosQueryParameters)
    {
        var result = await _stelexiService.GetStelexi(name, stelexosQueryParameters);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("StelexosByName")]
    public async Task<ActionResult<StelexosDtoBase>> GetStelexosByName(string name)
    {
        var result = await _stelexiService.GetStelexosByName(name, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("StelexosById")]
    public async Task<ActionResult<StelexosDtoBase>> GetStelexosById(int id)
    {
        var result = await _stelexiService.GetStelexosById(id, new());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("Stelexos")]
    public async Task<ActionResult<bool>> CreateStelexos([FromBody] CreateStelexosRequest request)
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
    public async Task<ActionResult<bool>> DeleteStelexos(int id)
    {
        var result = await _stelexiService.DeleteStelexos(id);
        if (!result)
            return BadRequest();
        return Ok(result);
    }

    //[HttpGet("Koinotarxes")]
    //public async Task<ActionResult<StelexosDtoBase>> GetKoinotarxes(string name, KoinotarxisQueryParameters stelexosQueryParameters)
    //{
    //    var result = await _stelexiService.GetStelexi(Thesi.Koinotarxis, name, stelexosQueryParameters);
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}

    //[HttpGet("KoinotarxisByName")]
    //public async Task<ActionResult<StelexosDtoBase>> GetKoinotarxisByName(string name)
    //{
    //    var result = await _stelexiService.GetStelexosByName(Thesi.Koinotarxis, name, new());
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}

    //[HttpGet("KoinotarxisById")]
    //public async Task<ActionResult<StelexosDtoBase>> GetKoinotarxisById(int id)
    //{
    //    var result = await _stelexiService.GetStelexosById(id, new());
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}

    //[HttpGet("Tomearxes")]
    //public async Task<ActionResult<StelexosDtoBase>> GetTomearxes(TomearxisQueryParameters stelexosQueryParameters)
    //{
    //    var result = await _stelexiService.GetStelexi(Thesi.Tomearxis, string.Empty, stelexosQueryParameters);
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}

    //[HttpGet("TomearxisByName")]
    //public async Task<ActionResult<StelexosDtoBase>> GetTomearxisByName(string name, TomearxisQueryParameters stelexosQueryParameters)
    //{
    //    var result = await _stelexiService.GetStelexosByName(Thesi.Tomearxis, name, stelexosQueryParameters);
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}

    //[HttpGet("TomearxisById")]
    //public async Task<ActionResult<StelexosDtoBase>> GetTomearxisById(int id)
    //{
    //    var result = await _stelexiService.GetStelexosById(id, new());
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}

    //[HttpDelete("Tomearxis")]
    //public async Task<ActionResult<bool>> DeleteTomearxis(int id)
    //{
    //    var result = await _stelexiService.DeleteStelexos(id);
    //    if (!result)
    //        return BadRequest();
    //    return Ok(result);
    //}

    //[HttpGet("Ekpaideutis")]
    //public async Task<ActionResult<StelexosDtoBase>> GetEkpaideutis(string name)
    //{
    //    var result = await _stelexiService.GetStelexi(Thesi.Ekpaideutis, name, null!);
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}

    //[HttpGet("EkpaideutisByName")]
    //public async Task<ActionResult<StelexosDtoBase>> GetEkpaideutisByName(string name)
    //{
    //    var result = await _stelexiService.GetStelexosByName(Thesi.Ekpaideutis, name, new());
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}

    //[HttpGet("EkpaideutisById")]
    //public async Task<ActionResult<StelexosDtoBase>> GetEkpaideutisById(int id)
    //{
    //    var result = await _stelexiService.GetStelexosById(id, new());
    //    if (result is null)
    //        return NotFound();
    //    return Ok(result);
    //}
}
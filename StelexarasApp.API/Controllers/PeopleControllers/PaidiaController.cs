using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class PaidiaController : ControllerBase
{
    private readonly IPaidiaService _paidiService;

    public PaidiaController(IPaidiaService paidiService)
    {
        _paidiService = paidiService;
    }

    [HttpGet("Paidia")]
    public async Task<ActionResult<IEnumerable<PaidiResponse>>> GetPaidia([FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaInService(null, paidiQueryParameters);
        return Ok(paidia);
    }

    [HttpGet("SearchByName/{name}")]
    public async Task<ActionResult<IEnumerable<PaidiResponse>>> SearchPaidiaByName(string name, [FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaByNameInService(name, paidiQueryParameters);
        return Ok(paidia);
    }
    
    [HttpGet("Kataskinotes")]
    public async Task<ActionResult<IEnumerable<PaidiResponse>>> GetKataskinotes([FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaInService(PaidiType.Kataskinotis, paidiQueryParameters);
        return Ok(paidia);
    }

    [HttpGet("Ekpaideuomenoi")]
    public async Task<ActionResult<IEnumerable<PaidiResponse>>> GetEkpaideuomenoi([FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaInService(PaidiType.Ekpaideuomenos, paidiQueryParameters);
        return Ok(paidia);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaidiResponse>> GetPaidi(int id, [FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _paidiService.GetPaidiByIdInService(id, paidiQueryParameters);
        if (result.IsFailed)
            return NotFound(result.Errors.First().Message);

        return result.Value;
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<PaidiResponse>> GetPaidiByName(string name, [FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _paidiService.GetPaidiByNameInService(name, paidiQueryParameters);
        if (result.IsFailed)
            return NotFound(result.Errors.First().Message);

        return result.Value;
    }    

    [HttpGet("Koinotita/ById/{id:int}")]
    public async Task<ActionResult<IEnumerable<PaidiResponse>>> GetPaidiaByKoinotitaId(int id, [FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaByKoinotitaIdInService(id, paidiQueryParameters);
        if (paidia == null)
            return NotFound();

        return Ok(paidia);
    }

    [HttpGet("Koinotita/ByName/{name}")]
    public async Task<ActionResult<IEnumerable<PaidiResponse>>> GetPaidiaByKoinotitaName(string name, [FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaByKoinotitaNameInService(name, paidiQueryParameters);
        if (paidia == null)
            return NotFound();

        return Ok(paidia);
    }

    [HttpGet("BySkiniId/{id:int}")]
    public async Task<ActionResult<IEnumerable<PaidiResponse>>> GetPaidiaBySkiniId(int id, [FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaBySkiniIdInService(id, paidiQueryParameters);

        return Ok(paidia);
    }

    [HttpPost]
    public async Task<ActionResult> PostPaidi([FromBody] CreatePaidiRequest createPaidiRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        var result = await _paidiService.CreatePaidiInService(createPaidiRequest);

        if (result.IsFailed)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdatePaidi(int id, [FromBody] UpdatePaidiRequest request)
    {
        request.Id = id;
        var result = await _paidiService.UpdatePaidiInService(request);

        if (!result.IsSuccess)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePaidi(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _paidiService.DeletePaidiInService(id);
        if (result.IsSuccess == false)
            return BadRequest(result.Errors.Select(e => e.Message));
        return Ok();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class PaidiaController : ControllerBase
{
    private readonly IPaidiaService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> _paidiService;

    public PaidiaController(IPaidiaService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> paidiService)
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

        var paidi = await _paidiService.GetPaidiByIdInService(id, paidiQueryParameters);
        if (paidi == null)
            return NotFound();

        return paidi;
    }

    [HttpGet("BySkiniId/{id:int}")]
    public async Task<ActionResult<PaidiResponse>> GetPaidiaBySkiniId(int id, [FromQuery] PaidiQueryParameters paidiQueryParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaBySkiniIdInService(id, paidiQueryParameters);

        return Ok(paidia);
    }

    [HttpPost]
    public async Task<bool> PostPaidi([FromBody] CreatePaidiRequest createPaidiRequest)
    {
        if (!ModelState.IsValid)
            return false;

        var result = await _paidiService.CreatePaidiInService(createPaidiRequest);

        if (result)
            return true;

        return false;
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<bool> PutPaidi(int id, [FromBody] UpdatePaidiRequest request)
    {
        request.Id = id;
        var result = await _paidiService.UpdatePaidiInService(request);

        if (!result)
            return false;

        return true;
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<bool> DeletePaidi(int id)
    {
        if (!ModelState.IsValid)
            return false;

        var result = await _paidiService.DeletePaidiInService(id);
        if (!result)
            return false;
        return true;
    }
}

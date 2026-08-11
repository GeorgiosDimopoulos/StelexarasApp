using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class PaidiaController : ControllerBase
{
    private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> _paidiService;

    public PaidiaController(IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, PaidiResponse> paidiService)
    {
        _paidiService = paidiService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaidiResponse>>> GetPaidia()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaInService(PaidiType.Kataskinotis);
        return Ok(paidia);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaidiResponse>> GetPaidi(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidi = await _paidiService.GetPaidiByIdInService(id);
        if (paidi == null)
            return NotFound();

        return paidi;
    }

    [HttpGet("BySkiniId/{id:int}")]
    public async Task<ActionResult<PaidiResponse>> GetPaidiaBySkiniId(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaBySkiniIdInService(id);

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

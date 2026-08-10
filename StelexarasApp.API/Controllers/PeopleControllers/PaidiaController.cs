using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class PaidiaController : ControllerBase
{
    private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiService;

    public PaidiaController(IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> paidiService)
    {
        _paidiService = paidiService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Kataskinotis>>> GetKataskinotes()
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


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Paidi>> PostKataskinotis([FromBody] CreatePaidiRequest createPaidiRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _paidiService.CreatePaidiInService(createPaidiRequest);

        if (result)
            return Ok(result);

        return NotFound();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutKataskinotis(int id, [FromBody] UpdatePaidiRequest request)
    {
        request.Id = id;
        var result = await _paidiService.UpdatePaidiInService(request);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteKataskinotis(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _paidiService.DeletePaidiInService(new DeletePaidiRequest { Id = id });
        if (!result)
            return NotFound();
        return Ok(result);
    }
}

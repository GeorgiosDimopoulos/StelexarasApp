using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StelexarasApp.Services.Interfaces.People;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
[Authorize]
internal class PaidiaController : ControllerBase
{
    private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiService;

    public PaidiaController(IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> paidiService)
    {
        _paidiService = paidiService;
    }

    [HttpGet("Paidia")]
    public async Task<ActionResult<IEnumerable<Kataskinotis>>> GetKataskinotes()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidia = await _paidiService.GetPaidiaInService(PaidiType.Kataskinotis);
        return Ok(paidia);
    }

    [HttpGet("Paidi/{id}")]
    public async Task<ActionResult<PaidiResponse>> GetPaidi(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var paidi = await _paidiService.GetPaidiByIdInService(id);
        if (paidi == null)
            return NotFound();

        return paidi;
    }

    [Authorize]
    [HttpPost("Paidi")]
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
    [HttpPut("Paidi/{id}")]
    public async Task<IActionResult> PutKataskinotis(int id, UpdatePaidiRequest request)
    {
        request.Id = id;
        var result = await _paidiService.UpdatePaidiInService(request);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKataskinotis(DeletePaidiRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _paidiService.DeletePaidiInService(request);
        if (!result)
            return NotFound();
        return Ok(result);
    }
}

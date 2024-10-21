using Microsoft.AspNetCore.Mvc;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web.Controllers.ApiControllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = false)]
public class DutiesController : ControllerBase
{
    private readonly IDutyService _dutyService;

    public DutiesController(IDutyService dutyService)
    {
        _dutyService = dutyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Duty>>> GetDuties()
    {
        try
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var duties = await _dutyService.GetDutiesInService();
            if (duties == null)
                return NotFound();
            return Ok(duties);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("Duty/{id}")]
    public async Task<ActionResult<Duty>> GetDutyById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var duty = await _dutyService.GetDutyByIdInService(id);
        if (duty == null)
            return NotFound();
        return Ok(duty);
    }

    [HttpPost("Duty")]
    public async Task<ActionResult<Duty>> PostDuty(Duty duty)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var dutyAdded = await _dutyService.AddDutyInService(duty);

        if (dutyAdded == false)
            return NotFound();
        return Ok(dutyAdded);
    }

    [HttpPut("Duty/{id}")]
    public async Task<IActionResult> PutDuty(int id, Duty duty)
    {
        if (id != duty.Id)
            return BadRequest();

        var isUpdated = await _dutyService.UpdateDutyInService(duty.Name, duty);
        if (isUpdated == false)
            return NotFound();
        return Ok(isUpdated);
    }

    [HttpDelete("Duty/{id}")]
    public async Task<IActionResult> DeleteDuty(int id)
    {
        var isDeleted = await _dutyService.DeleteDutyInService(id);
        if (isDeleted == false)
            return NotFound();
        return Ok(isDeleted);
    }
}

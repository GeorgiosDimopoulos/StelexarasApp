using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos;
using StelexarasApp.Services.Interfaces;

namespace StelexarasApp.API.Controllers.GeneralControllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DutiesController : ControllerBase
{
    private readonly IDutyService _dutyService;

    public DutiesController(IDutyService dutyService)
    {
        _dutyService = dutyService;
    }

    /// <summary>
    ///  Get all the current duties
    /// </summary>
    /// <returns>JWT token + expiration if OK, l, or an error result</returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DutyResponse>>> GetDuties()
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
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return BadRequest();
        }
    }

    [AllowAnonymous]
    [HttpGet("Duty/{id}")]
    public async Task<ActionResult<DutyResponse>> GetDutyById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var duty = await _dutyService.GetDutyByIdInService(id);
        if (duty == null)
            return NotFound();
        return Ok(duty);
    }

    [HttpPost("Duty")]
    public async Task<ActionResult<bool>> PostDuty([FromBody] CreateDutyRequest duty)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var dutyAdded = await _dutyService.AddDutyInService(duty);

        if (dutyAdded == false)
            return NotFound();
        return Ok(dutyAdded);
    }

    [HttpPut("Duty")]
    public async Task<IActionResult> PutDuty([FromBody] UpdateDutyRequest duty)
    {
        var isUpdated = await _dutyService.UpdateDutyInService(duty);
        if (isUpdated == false)
            return NotFound();
        return Ok(isUpdated);
    }

    [HttpDelete("Duty/")]
    public async Task<IActionResult> DeleteDuty([FromQuery] DeleteDutyRequest request)
    {
        var isDeleted = await _dutyService.DeleteDutyInService(request);
        if (isDeleted == false)
            return NotFound();
        return Ok(isDeleted);
    }
}

using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.People;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class ParentsController(IParentsService parentsService) : ControllerBase
{
    private readonly IParentsService _parentsService = parentsService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParentResponse>>> GetParents([FromQuery] ParentQueryParameters parentQueryParameters)
    {
        var parents = await _parentsService.GetParentsInService(parentQueryParameters);
        if (parents.IsFailed)
            return BadRequest(parents.Errors.Select(e => e.Message));
        return Ok(parents.Value);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ParentResponse>> GetParentById(int id, [FromQuery] ParentQueryParameters parentQueryParameters)
    {
        if (id <= 0)
            return BadRequest("Invalid parent id.");

        var parents = await _parentsService.GetParentByIdInService(id, parentQueryParameters);
        if (parents.IsFailed)
            return NotFound(parents.Errors.First().Message);
        return Ok(parents.Value);
    }

    [HttpGet("ByName/{name}")]
    public async Task<ActionResult<ParentResponse>> GetParentByName(string name, [FromQuery] ParentQueryParameters parentQueryParameters)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Invalid parent name.");

        var parents = await _parentsService.GetParentByNameInService(name, parentQueryParameters);
        if (parents.IsFailed)
            return NotFound(parents.Errors.First().Message);
        return Ok(parents.Value);
    }

    [HttpGet("ByChild/{id:int}")]
    public async Task<ActionResult<ParentResponse>> GetParentByChildId(int id, [FromQuery] ParentQueryParameters parentQueryParameters)
    {
        if (id <= 0)
            return BadRequest("Invalid parent id.");

        var parents = await _parentsService.GetParentByChildIdInService(id, parentQueryParameters);
        if (parents.IsFailed)
            return NotFound(parents.Errors.First().Message);
        return Ok(parents.Value);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateParent([FromBody] CreateParentRequest createParentRequest)
    {
        var parentCreated = await _parentsService.CreateParentInService(createParentRequest);
        if (parentCreated.IsFailed)
            return BadRequest(parentCreated.Errors.Select(e => e.Message));
        return Ok(true);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<bool>> UpdateParent(int id, [FromBody] UpdateParentRequest parentUpdateRequest)
    {
        if (id <= 0)
            return BadRequest("Invalid parent id.");

        var parentUpdated = await _parentsService.UpdateParentInService(id, parentUpdateRequest);
        if (parentUpdated.IsFailed)
            return BadRequest(parentUpdated.Errors.Select(e => e.Message));

        return Ok(true);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteParent(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid parent id.");

        var parentDeleted = await _parentsService.DeleteParentInService(id);
        if (parentDeleted.IsFailed)
            return NotFound(parentDeleted.Errors.First().Message);
        return Ok(true);
    }
}

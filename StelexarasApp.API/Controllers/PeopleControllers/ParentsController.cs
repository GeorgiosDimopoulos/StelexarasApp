using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.People;

namespace StelexarasApp.API.Controllers.PeopleControllers;

[ApiController]
[Route("[controller]")]
public class ParentsController(IParentsService parentsService) : ControllerBase
{
    private readonly IParentsService _parentsService = parentsService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParentResponse>>> GetParents(ParentQueryParameters parentQueryParameters)
    {
        var parents = await _parentsService.GetParentsInService(parentQueryParameters);
        return Ok(parents);
    }


    [HttpGet]
    public async Task<ActionResult<ParentResponse>> GetParentById(int id, ParentQueryParameters parentQueryParameters)
    {
        if (id <= 0)
            return BadRequest("Invalid parent id.");

        var parents = await _parentsService.GetParentByIdInService(id, parentQueryParameters);
        return Ok(parents);
    }

    [HttpGet]
    public async Task<ActionResult<ParentResponse>> GetParentByName(string name, ParentQueryParameters parentQueryParameters)
    {
        if (string.IsNullOrEmpty(name))
            return BadRequest("Invalid parent name.");

        var parents = await _parentsService.GetParentByNameInService(name, parentQueryParameters);
        return Ok(parents);
    }

    [HttpGet]
    public async Task<ActionResult<ParentResponse>> GetParentByChildId(int id, ParentQueryParameters parentQueryParameters)
    {
        if (id <= 0)
            return BadRequest("Invalid parent id.");

        var parents = await _parentsService.GetParentByChildIdInService(id, parentQueryParameters);
        return Ok(parents);
    }

    [HttpPost]
    public async Task<ActionResult<ParentResponse>> CreateParent(CreateParentRequest createParentRequest)
    {
        var parents = await _parentsService.CreateParentInService(createParentRequest);
        return Ok(parents);
    }

    [HttpPut]
    public async Task<ActionResult<ParentResponse>> UpdateParent(int id, UpdateParentRequest parentUpdateRequest)
    {
        if (id <= 0)
            return BadRequest("Invalid parent id.");

        var parents = await _parentsService.UpdateParentInService(id, parentUpdateRequest);
        return Ok(parents);
    }

    [HttpDelete]
    public async Task<ActionResult<ParentResponse>> DeleteParent(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid parent id.");

        var parents = await _parentsService.DeleteParentInService(id);
        return Ok(parents);
    }
}

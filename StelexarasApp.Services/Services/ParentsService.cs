using AutoMapper;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using StelexarasApp.Library.Dtos.People;
using StelexarasApp.Services.Validators;

namespace StelexarasApp.Services.Services;

public class ParentsService : IParentsService
{
    private readonly ILogger<ParentsService> _logger;
    private readonly IParentsRepository _parentsRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ParentDtoBase> _validator;

    public ParentsService(ILogger<ParentsService> logger, IParentsRepository parentsRepository, IMapper mapper, IValidator<ParentDtoBase> validator)
    {
        _logger = logger;
        _parentsRepository = parentsRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<ParentResponse>>> GetParentsInService(ParentQueryParameters parentQueryParameters)
    {
        var parents = await _parentsRepository.GetParentsInDb(parentQueryParameters);
        if (parents == null)
            return Result.Fail("No parents found.");
        var parentsResponse = _mapper.Map<IEnumerable<ParentResponse>>(parents);
        return Result.Ok(parentsResponse);
    }

    public async Task<Result<ParentResponse>> GetParentByIdInService(int id, ParentQueryParameters parentQueryParameters)
    {
        var parent = await _parentsRepository.GetParentByIdInDb(id, parentQueryParameters);
        if (parent == null)
            return Result.Fail("Parent not found.");
        var parentResponse = _mapper.Map<ParentResponse>(parent);
        return Result.Ok(parentResponse);
    }

    public async Task<Result<ParentResponse>> GetParentByChildIdInService(int childId, ParentQueryParameters parentQueryParameters)
    {
        var parent = await _parentsRepository.GetParentByChildIdInDb(childId, parentQueryParameters);
        if (parent == null)
            return Result.Fail("Parent not found.");
        var parentResponse = _mapper.Map<ParentResponse>(parent);
        return Result.Ok(parentResponse);
    }

    public async Task<Result<ParentResponse>> GetParentByNameInService(string name, ParentQueryParameters parentQueryParameters)
    {
        var parent = await _parentsRepository.GetParentByNameInDb(name, parentQueryParameters);
        if (parent == null)
            return Result.Fail("Parent not found.");
        var parentResponse = _mapper.Map<ParentResponse>(parent);
        return Result.Ok(parentResponse);
    }

    public async Task<Result> CreateParentInService(CreateParentRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.FirstName))
        {
            return Result.Fail("Invalid parent request.");
        }

        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            return Result.Fail(validation.Errors.Select(e => e.ErrorMessage).ToArray());
        }

        var parent = _mapper.Map<Parent>(request);
        var result = _parentsRepository.AddParentInDb(parent);
        return Result.Ok();
    }

    public async Task<Result> DeleteParentInService(int id)
    {
        var existingParent = await _parentsRepository.GetParentByIdInDb(id, new() { IncludePaidi = false });
        if (existingParent == null)
            return Result.Fail("Parent not found.");

        var res = await _parentsRepository.DeleteParentInDb(id);
        if (!res)
            return Result.Fail("Failed to delete parent.");

        return Result.Ok();
    }

    public async Task<Result> UpdateParentInService(int id, UpdateParentRequest request)
    {
        var parent = _mapper.Map<Parent>(request);
        parent.Id = id;

        var res = await _parentsRepository.UpdateParentInDb(parent.Id, parent);
        if (!res)
            return Result.Fail("Failed to update parent.");

        return Result.Ok();
    }
}
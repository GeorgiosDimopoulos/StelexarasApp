using AutoMapper;
using StelexarasApp.Library.Dtos.People;

namespace StelexarasApp.Services.Mappers.People;

public class ParentMappingProfile : Profile
{
    public ParentMappingProfile()
    {
        CreateMap<Parent, ParentResponse>();
        CreateMap<CreateParentRequest, Parent>();
        CreateMap<UpdateParentRequest, Parent>();
    }
}
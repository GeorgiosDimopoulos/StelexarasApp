using AutoMapper;

namespace StelexarasApp.Services.Mappers.People.Staff;

public class TomearxisMappingProfile : Profile
{
    public TomearxisMappingProfile()
    {
        CreateMap<CreateTomearxisRequest, Tomearxis>()
            .IncludeBase<IStelexosDto, IStelexos>()
            .ForMember(dest => dest.Koinotarxes, opt => opt.Ignore())
            .ReverseMap();
    }
}

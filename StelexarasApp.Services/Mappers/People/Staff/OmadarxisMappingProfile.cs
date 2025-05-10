using AutoMapper;

namespace StelexarasApp.Services.Mappers.People.Staff;

public class OmadarxisMappingProfile : Profile
{
    public OmadarxisMappingProfile()
    {

        CreateMap<CreateOmadarxisRequest, Omadarxis>()
            .IncludeBase<IStelexosDto, IStelexos>()
            .ForMember(dest => dest.Skini, opt => opt.MapFrom(src => new Skini { Name = src.XwrosName ?? string.Empty }))
            .ReverseMap();
    }
}

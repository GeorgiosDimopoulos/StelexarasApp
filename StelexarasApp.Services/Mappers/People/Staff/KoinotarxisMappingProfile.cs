using AutoMapper;

namespace StelexarasApp.Services.Mappers.People.Staff;

public class KoinotarxisMappingProfile : Profile
{
    public KoinotarxisMappingProfile()
    {
        CreateMap<CreateKoinotarxisRequest, Koinotarxis>()
            .IncludeBase<IStelexosDto, IStelexos>()
            .ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Name = src.XwrosName ?? string.Empty }))
            .ReverseMap();
    }
}

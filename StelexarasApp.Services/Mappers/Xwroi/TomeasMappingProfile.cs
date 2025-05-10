using AutoMapper;

namespace StelexarasApp.Services.Mappers.Xwroi;

public class TomeasMappingProfile : Profile
{
    public TomeasMappingProfile()
    {
        CreateMap<CreateTomeasRequest, Tomeas>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TomearxisId, opt => opt.Ignore())
            .ForMember(dest => dest.Tomearxis, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotites, opt => opt.Ignore());

        CreateMap<UpdateTomeasRequest, Tomeas>()
            .ForMember(dest => dest.TomearxisId, opt => opt.Ignore())
            .ForMember(dest => dest.Tomearxis, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotites, opt => opt.Ignore());

        CreateMap<Tomeas, TomeasResponse>()
            .ForMember(dest => dest.KoinotitesNumber, opt => opt.MapFrom((src, dest) => src.Koinotites != null ? src.Koinotites.Count() : 0));
    }
}

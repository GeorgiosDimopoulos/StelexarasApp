using AutoMapper;

namespace StelexarasApp.Services.Mappers.Teams;

public class SkiniMappingProfile : Profile
{
    public SkiniMappingProfile()
    {
        CreateMap<CreateSkiniRequest, Skini>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.Omadarxis, opt => opt.Ignore())
          .ForMember(dest => dest.Paidia, opt => opt.Ignore())
          .ForMember(dest => dest.OmadarxisId, opt => opt.Ignore())
          .ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Name = src.KoinotitaName ?? string.Empty }));

        CreateMap<UpdateSkiniRequest, Skini>()
            .ForMember(dest => dest.Omadarxis, opt => opt.Ignore())
            .ForMember(dest => dest.Paidia, opt => opt.Ignore())
            .ForMember(dest => dest.OmadarxisId, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Name = src.KoinotitaName ?? string.Empty }));

        CreateMap<Skini, SkiniResponse>()
            .ForMember(dest => dest.PaidiaNumber, opt => opt.MapFrom(src => src.Paidia != null ? src.Paidia.Count : 0))
            .ForMember(dest => dest.KoinotitaName, opt => opt.MapFrom(src => src.Koinotita != null ? src.Koinotita.Name : string.Empty));
    }
}

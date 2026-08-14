using AutoMapper;

namespace StelexarasApp.Services.Mappers.Teams;

public class KoinotitaMappingProfile : Profile
{
    public KoinotitaMappingProfile()
    {
        CreateMap<CreateKoinotitaRequest, Koinotita>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.KoinotarxisId, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotarxis, opt => opt.Ignore())
            .ForMember(dest => dest.Skines, opt => opt.Ignore())
            .ForMember(dest => dest.Tomeas, opt => opt.MapFrom(src => new Tomeas { Name = src.TomeasName ?? string.Empty }));

        CreateMap<UpdateKoinotitaRequest, Koinotita>()
            .ForMember(dest => dest.KoinotarxisId, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotarxis, opt => opt.Ignore())
            .ForMember(dest => dest.Skines, opt => opt.Ignore());

        CreateMap<Koinotita, KoinotitaResponse>()
            .ForMember(dest => dest.KoinotarxisName, opt => opt.MapFrom(src => src.Koinotarxis != null ? $"{src.Koinotarxis.LastName} {src.Koinotarxis.FirstName}" : string.Empty))
            .ForMember(dest => dest.SkinesNumber, opt => opt.MapFrom((src, dest) => src.Skines != null ? src.Skines.Count : 0))
            .ForMember(dest => dest.TomeasName, opt => opt.MapFrom(src => src.Tomeas != null ? src.Tomeas.Name : string.Empty));
    }
}

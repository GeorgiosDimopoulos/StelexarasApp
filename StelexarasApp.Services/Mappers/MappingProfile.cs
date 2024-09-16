using StelexarasApp.DataAccess.Models.Atoma.Staff;
using AutoMapper;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels.Atoma;

namespace StelexarasApp.Services.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Stelexos, OmadarxisDto>();
        CreateMap<StelexosDto, OmadarxisDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            .ForMember(dest => dest.Sex, opt => opt.Ignore())
            .ForMember(dest => dest.SkiniId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => Thesi.Omadarxis));

        CreateMap<StelexosDto, KoinotarxisDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            .ForMember(dest => dest.Sex, opt => opt.Ignore())
            .ForMember(dest => dest.KoinotitaId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => Thesi.Omadarxis));

        CreateMap<StelexosDto, TomearxisDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            .ForMember(dest => dest.Sex, opt => opt.Ignore())
            .ForMember(dest => dest.TomeasId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => Thesi.Omadarxis));

        CreateMap<TomearxisDto, Tomearxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Tomeas, opt => opt.MapFrom(src => new Tomeas { Id = src.TomeasId }))
            .ForMember(dest => dest.Koinotarxes, opt => opt.Ignore()).ReverseMap();

        CreateMap<KoinotarxisDto, Koinotarxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Id = src.KoinotitaId ?? 0 }))
            .ForMember(dest => dest.Omadarxes, opt => opt.Ignore()).ReverseMap();

        CreateMap<OmadarxisDto, Omadarxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.Skini, opt => opt.MapFrom(src => new Skini { Id = src.SkiniId }))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => src.Thesi)).ReverseMap();

        CreateMap<StelexosDto, Stelexos>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

        CreateMap<EkpaideutisDto, Ekpaideutis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
    }
}
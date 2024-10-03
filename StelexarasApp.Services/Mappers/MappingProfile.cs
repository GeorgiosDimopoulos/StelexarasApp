using StelexarasApp.DataAccess.Models.Atoma.Staff;
using AutoMapper;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Services.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<StelexosDto, StelexosBase>();

        CreateMap<TomearxisDto, Tomearxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.Tomeas, opt => opt.MapFrom(src => new Tomeas { Id = src.TomeasId }))
            .ForMember(dest => dest.Koinotarxes, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<KoinotarxisDto, Koinotarxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel))
            .ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Id = src.KoinotitaId ?? 0 }))
            .ForMember(dest => dest.Omadarxes, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<OmadarxisDto, Omadarxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.Skini, opt => opt.MapFrom(src => new Skini { Id = src.SkiniId }))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? 0))
            .ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => src.Thesi))
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel)).
            ReverseMap();

        CreateMap<EkpaideutisDto, Ekpaideutis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            .ForMember(dest => dest.Thesi, opt => opt.Ignore())
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel))
            .ReverseMap();

        CreateMap<TomeasDto, Tomeas>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TomearxisId, opt => opt.Ignore())
            .ForMember(dest => dest.Tomearxis, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotites, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<SkiniDto, Skini>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Omadarxis, opt => opt.Ignore())
            .ForMember(dest => dest.Paidia, opt => opt.MapFrom(src => new List<Paidi>(new Paidi [src.PaidiaNumber])))
            .ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Name = src.KoinotitaName ?? string.Empty }))
            .ForMember(dest => dest.OmadarxisId, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<KoinotitaDto, Koinotita>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotarxis, opt => opt.Ignore())
            .ForMember(dest => dest.KoinotarxisId, opt => opt.Ignore())
            .ForMember(dest => dest.Tomeas, opt => opt.Ignore())
            .ForMember(dest => dest.Skines, opt => opt.MapFrom(src => new List<Skini>(new Skini [src.SkinesNumber])))
            .ReverseMap();
    }
}
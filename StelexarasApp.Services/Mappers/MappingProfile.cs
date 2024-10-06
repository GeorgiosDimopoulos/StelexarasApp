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
        // CreateMap<StelexosDto, StelexosBase>();

        CreateMap<Omadarxis, StelexosBase>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.XwrosName, opt => opt.MapFrom(src => src.Skini.Name));

        CreateMap<Tomearxis, StelexosBase>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom<NonNullIdResolver>())
            .ForMember(dest => dest.FullName, opt => opt.MapFrom<NonNullNameResolver>())
            .ForMember(dest => dest.Tel, opt => opt.MapFrom<NonNullTelResolver>())
            .ForMember(dest => dest.Age, opt => opt.MapFrom<NonNullAgeResolver>())
            .ForMember(dest => dest.Sex, opt => opt.MapFrom<NonNullSexResolver>())
            .ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => src.Thesi))
            .ForMember(dest => dest.XwrosName, opt => opt.MapFrom(src => src.Tomeas.Name))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FullName, opt => opt.Ignore())
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            .ForMember(dest => dest.Sex, opt => opt.Ignore())
            .ForMember(dest => dest.Tel, opt => opt.Ignore())
            .ForMember(dest => dest.Thesi, opt => opt.Ignore())
            .ForMember(dest => dest.Tomeas, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotarxes, opt => opt.Ignore());

        CreateMap<TomearxisDto, Tomearxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel ?? string.Empty))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.XwrosName, opt => opt.MapFrom(src => src.DtoXwrosName ?? string.Empty))
            //.ForMember(dest => dest.Tomeas, opt => opt.MapFrom(src => new Tomeas { Name = src.FullName }))
            .ForMember(dest => dest.Koinotarxes, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<KoinotarxisDto, Koinotarxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.XwrosName, opt => opt.MapFrom(src => src.DtoXwrosName))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel))
            //.ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Id = src.KoinotitaId ?? 0 }))
            .ForMember(dest => dest.Omadarxes, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<OmadarxisDto, Omadarxis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.XwrosName, opt => opt.MapFrom(src => src.DtoXwrosName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            //.ForMember(dest => dest.Skini, opt => opt.MapFrom(src => new Skini { Id = src.SkiniId }))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? 0))
            .ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => src.Thesi))
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel)).
            ReverseMap();

        CreateMap<EkpaideutisDto, Ekpaideutis>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => src.Thesi))
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel))
            .ReverseMap();

        CreateMap<TomeasDto, Tomeas>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TomearxisId, opt => opt.Ignore())
            .ForMember(dest => dest.Tomearxis, opt => opt.Ignore())
            .ForMember(dest => dest.Koinotites, opt => opt.MapFrom(src => new Tomeas
            {
                Name = src.Name,
                Koinotites = new List<Koinotita>(src.KoinotitesNumber)
            }))
            .ReverseMap()
            .ForMember(dest => dest.KoinotitesNumber, opt => opt.MapFrom(src => src.Koinotites != null ? src.Koinotites.Count() : 0));

        CreateMap<Tomeas, TomeasDto>()
            .ForMember(dest => dest.KoinotitesNumber, opt => opt.MapFrom(src => src.Koinotites != null ? src.Koinotites.Count() : 0))
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
            .ReverseMap()
            .ForMember(dest => dest.TomeasName, opt => opt.MapFrom(src => src.Tomeas.Name))
            .ForMember(dest => dest.SkinesNumber, opt => opt.MapFrom(src => src.Skines.Count()));
    }
}
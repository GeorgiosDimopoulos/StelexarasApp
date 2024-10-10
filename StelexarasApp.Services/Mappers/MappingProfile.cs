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
        CreateMap<IStelexosDto, IStelexos>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            //.ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel))
            //.ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            //.ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            //.ForMember(dest => dest.Thesi, opt => opt.MapFrom(src => src.Thesi))
            .ReverseMap();

        CreateMap<TomearxisDto, Tomearxis>()
            .IncludeBase<IStelexosDto, IStelexos>()
            //.ForMember(dest => dest.Koinotarxes, opt => opt.MapFrom(src => src.KoinotarxesIds != null ?
            //src.KoinotarxesIds.Select(id => new Koinotarxis
            //{
            //    Id = id,
            //    Thesi = Thesi.Tomearxis,
            //    Tel = src.Tel ?? string.Empty,
            //    Age = src.Age,
            //    FullName = src.FullName ?? string.Empty,
            //    Sex = src.Sex
            //}) : new List<Koinotarxis>()))                        
            .ForMember(dest => dest.Koinotarxes, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<OmadarxisDto, Omadarxis>()
            .IncludeBase<IStelexosDto, IStelexos>()
            .ForMember(dest => dest.XwrosName, opt => opt.MapFrom(src => src.DtoXwrosName))
            .ForMember(dest => dest.Skini, opt => opt.MapFrom(src => new Skini { Id = src.SkiniId }))
            .ReverseMap();

        CreateMap<KoinotarxisDto, Koinotarxis>()
            .IncludeBase<IStelexosDto, IStelexos>()
            .ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Id = src.KoinotitaId ?? 0 }))
            .ReverseMap();

        CreateMap<EkpaideutisDto, Ekpaideutis>()
            .IncludeBase<IStelexosDto, IStelexos>()
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

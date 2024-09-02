using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using AutoMapper;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.DtosModels.Atoma;

namespace StelexarasApp.Services.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TomearxisDto, Tomearxis>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Tomeas, opt => opt.MapFrom(src => new Tomeas { Id = src.TomeasId }))
                .ForMember(dest => dest.Koinotarxes, opt => opt.Ignore());

            CreateMap<KoinotarxisDto, Koinotarxis>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Koinotita, opt => opt.MapFrom(src => new Koinotita { Id = src.KoinotitaId }))
                .ForMember(dest => dest.Omadarxes, opt => opt.Ignore());

            CreateMap<OmadarxisDto, Omadarxis>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Skini, opt => opt.MapFrom(src => new Skini { Id = src.SkiniId }));
                // .ForMember(dest => dest.Paidia, opt => opt.Ignore());
        }
    }
}
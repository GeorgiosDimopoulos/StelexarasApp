using StelexarasApp.DataAccess.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using AutoMapper;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Services.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TomearxisDto, Tomearxis>()
                .ForMember(dest => dest.Tomeas, opt => opt.MapFrom(src => new Tomeas { Id = src.TomeasId }))
                .ForMember(dest => dest.Koinotarxes, opt => opt.Ignore());
        }
    }
}
using AutoMapper;

namespace StelexarasApp.Services.Mappers.People;

public class StelexossMappingProfile : Profile
{
    public StelexossMappingProfile()
    {
        CreateMap<StelexosDtoBase, IStelexos>()
               .IncludeBase<IStelexosDto, IStelexos>()
               .ReverseMap();
    }          
}

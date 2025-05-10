using AutoMapper;

namespace StelexarasApp.Services.Mappers.People.Staff;

public class EkpaideutisMappingProfile : Profile
{
    public EkpaideutisMappingProfile()
    {
        CreateMap<EkpaideutisDtoBase, Ekpaideutis>()
               .IncludeBase<IStelexosDto, IStelexos>()
               .ReverseMap();
    }          
}

using AutoMapper;

namespace StelexarasApp.Services.Mappers.People;

public class PaidiMappingProfile : Profile
{
    public PaidiMappingProfile()
    {
        CreateMap<CreatePaidiRequest, Kataskinotis>();
        CreateMap<CreatePaidiRequest, Ekpaideuomenos>();

        CreateMap<Kataskinotis, PaidiResponse>();
        CreateMap<Ekpaideuomenos, PaidiResponse>();
    }
}

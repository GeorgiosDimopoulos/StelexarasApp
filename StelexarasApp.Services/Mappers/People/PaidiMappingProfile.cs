using AutoMapper;

namespace StelexarasApp.Services.Mappers.People;

public class PaidiMappingProfile : Profile
{
    public PaidiMappingProfile()
    {
        CreateMap<Paidi,PaidiResponse>()
            .Include<Kataskinotis, PaidiResponse>()
            .Include<Ekpaideuomenos, PaidiResponse>();
        CreateMap<CreatePaidiRequest, Kataskinotis>();
        CreateMap<CreatePaidiRequest, Ekpaideuomenos>();

        CreateMap<Kataskinotis, PaidiResponse>();
        CreateMap<Ekpaideuomenos, PaidiResponse>();

        CreateMap<UpdatePaidiRequest, Paidi>();
        CreateMap<UpdatePaidiRequest, Kataskinotis>();
        CreateMap<UpdatePaidiRequest, Ekpaideuomenos>();
    }
}

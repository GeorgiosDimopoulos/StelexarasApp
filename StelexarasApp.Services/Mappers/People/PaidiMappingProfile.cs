using AutoMapper;

namespace StelexarasApp.Services.Mappers.People;

public class PaidiMappingProfile : Profile
{
    public PaidiMappingProfile()
    {
        CreateMap<Paidi, PaidiDtoBase>();

        CreateMap<Paidi, PaidiResponse>()
            .Include<Kataskinotis, PaidiResponse>()
            .Include<Ekpaideuomenos, PaidiResponse>()
            .ForMember(d => d.ParentTel, o => o.MapFrom(s => s.Parent.Phone));

        CreateMap<Kataskinotis, PaidiResponse>();
        CreateMap<Ekpaideuomenos, PaidiResponse>();

        CreateMap<UpdatePaidiRequest, Paidi>().ForMember(d => d.Parent.Phone, o => o.MapFrom(s => s.ParentTel));
        CreateMap<UpdatePaidiRequest, Kataskinotis>().ForMember(d => d.Parent.Phone, o => o.MapFrom(s => s.ParentTel));
        CreateMap<UpdatePaidiRequest, Ekpaideuomenos>().ForMember(d => d.Parent.Phone, o => o.MapFrom(s => s.ParentTel));

        CreateMap<CreatePaidiRequest, Paidi>().ForMember(d => d.Parent.Phone, o => o.MapFrom(s => s.ParentTel));
        CreateMap<CreatePaidiRequest, Kataskinotis>().ForMember(d => d.Parent.Phone, o => o.MapFrom(s => s.ParentTel));
        CreateMap<CreatePaidiRequest, Ekpaideuomenos>().ForMember(d => d.Parent.Phone, o => o.MapFrom(s => s.ParentTel));
    }
}

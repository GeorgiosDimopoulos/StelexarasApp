using AutoMapper;

namespace StelexarasApp.Services.Mappers.People;

public class PaidiMappingProfile : Profile
{
    public PaidiMappingProfile()
    {
        CreateMap<Paidi, PaidiResponse>()
            .ForMember(d => d.ParentTel, o => o.MapFrom(s => s.Parent != null ? s.Parent.Phone : string.Empty))
            .Include<Kataskinotis, PaidiResponse>()
            .Include<Ekpaideuomenos, PaidiResponse>();
        CreateMap<Kataskinotis, PaidiResponse>();
        CreateMap<Ekpaideuomenos, PaidiResponse>();

        CreateMap<CreatePaidiRequest, Kataskinotis>().ForPaidiRequestMembers();
        CreateMap<CreatePaidiRequest, Ekpaideuomenos>().ForPaidiRequestMembers();
        CreateMap<UpdatePaidiRequest, Paidi>().ForPaidiRequestMembers();
    }
}

internal static class PaidiMappingExtensions
{
    public static IMappingExpression<TSource, TPaidi> ForPaidiRequestMembers<TSource, TPaidi>(
        this IMappingExpression<TSource, TPaidi> map)
        where TSource : PaidiDtoBase
        where TPaidi : Paidi
    {
        return map
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.SkiniId, o => o.Ignore())
            .ForMember(d => d.Skini, o => o.Ignore())
            .ForMember(d => d.ParentId, o => o.Ignore())
            .ForMember(d => d.Parent, o => o.MapFrom(s => new Parent { Phone = s.ParentTel.Trim() }));
    }
}

using AutoMapper;

namespace StelexarasApp.Services.Mappers.People;

public class StelexosMappingProfile : Profile
{
    public StelexosMappingProfile()
    {
        CreateMap<StelexosDtoBase, Omadarxis>().ReverseMap();

        CreateMap<CreateStelexosRequest, Omadarxis>()
            .IncludeBase<StelexosDtoBase, Omadarxis>();

        CreateMap<UpdateStelexosRequest, Omadarxis>()
            .IncludeBase<StelexosDtoBase, Omadarxis>();

        CreateMap<DeleteStelexosRequest, Omadarxis>();

        CreateMap<StelexosResponse, Omadarxis>()
            .IncludeBase<StelexosDtoBase, Omadarxis>()
            .ReverseMap();

        CreateMap<StelexosDtoBase, Koinotarxis>().ReverseMap();

        CreateMap<CreateStelexosRequest, Koinotarxis>()
            .IncludeBase<StelexosDtoBase, Koinotarxis>();

        CreateMap<UpdateStelexosRequest, Koinotarxis>()
            .IncludeBase<StelexosDtoBase, Koinotarxis>();

        CreateMap<DeleteStelexosRequest, Koinotarxis>();

        CreateMap<StelexosResponse, Koinotarxis>()
            .IncludeBase<StelexosDtoBase, Koinotarxis>()
            .ReverseMap();

        CreateMap<StelexosDtoBase, Tomearxis>().ReverseMap();

        CreateMap<CreateStelexosRequest, Tomearxis>()
            .IncludeBase<StelexosDtoBase, Tomearxis>();

        CreateMap<UpdateStelexosRequest, Tomearxis>()
            .IncludeBase<StelexosDtoBase, Tomearxis>();

        CreateMap<DeleteStelexosRequest, Tomearxis>();

        CreateMap<StelexosResponse, Tomearxis>()
            .IncludeBase<StelexosDtoBase, Tomearxis>()
            .ReverseMap();

        CreateMap<StelexosDtoBase, Ekpaideutis>().ReverseMap();

        CreateMap<CreateStelexosRequest, Ekpaideutis>()
            .IncludeBase<StelexosDtoBase, Ekpaideutis>();

        CreateMap<UpdateStelexosRequest, Ekpaideutis>()
            .IncludeBase<StelexosDtoBase, Ekpaideutis>();

        CreateMap<DeleteStelexosRequest, Ekpaideutis>();

        CreateMap<StelexosResponse, Ekpaideutis>()
            .IncludeBase<StelexosDtoBase, Ekpaideutis>()
            .ReverseMap();
    }
}

using AutoMapper;

namespace StelexarasApp.Services.Mappers;

public class OthersMappingProfiles : Profile
{
    public OthersMappingProfiles()
    {
        CreateMap<CreateDutyRequest, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore());

        CreateMap<UpdateDutyRequest, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore());

        CreateMap<DeleteDutyRequest, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<DutyResponse, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();
    }
}

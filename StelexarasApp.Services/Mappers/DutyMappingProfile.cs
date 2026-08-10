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

        CreateMap<DutyResponse, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();
    }
}

using AutoMapper;
using StelexarasApp.Library.Dtos;

namespace StelexarasApp.Services.Mappers;

public class OthersMappingProfiles : Profile
{
    public OthersMappingProfiles()
    {
        CreateMap<CreateDutyRequest, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<UpdateDutyRequest, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<DeleteDutyRequest, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();
    }
}

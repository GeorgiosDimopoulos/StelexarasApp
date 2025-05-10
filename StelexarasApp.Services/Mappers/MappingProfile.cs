using AutoMapper;
using StelexarasApp.Library.Dtos;

namespace StelexarasApp.Services.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDutyRequest, Duty>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<CreateExpenseRequest, Expense>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();
    }
}

using AutoMapper;
using StelexarasApp.Library.Dtos;

namespace StelexarasApp.Services.Mappers;

public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        CreateMap<CreateExpenseRequest, Expense>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<UpdateExpenseRequest, Expense>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<DeleteExpenseRequest, Expense>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Expense, ExpenseResponse>();
    }
}

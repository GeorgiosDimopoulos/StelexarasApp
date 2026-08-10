using AutoMapper;

namespace StelexarasApp.Services.Mappers;

public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        CreateMap<CreateExpenseRequest, Expense>()
            .ForMember(dest => dest.Date, opt => opt.Ignore());

        CreateMap<UpdateExpenseRequest, Expense>()
            .ForMember(dest => dest.Date, opt => opt.Ignore());

        CreateMap<Expense, ExpenseResponse>()
            .ReverseMap();
    }
}

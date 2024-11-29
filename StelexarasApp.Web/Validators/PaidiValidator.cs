using FluentValidation;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Models.Atoma;

namespace StelexarasApp.Web.Validators;

public class PaidiValidator : AbstractValidator<PaidiDto>
{
    public PaidiValidator()
    {
        RuleFor(user => user.FullName)
            .NotEmpty().WithMessage("PaidiDto Name is required")
            .Length(5, 50).WithMessage("PaidiDto Name must be between 2 and 50 characters");

        RuleFor(user => user.SkiniName)
            .NotEmpty().WithMessage("PaidiDto SkiniName is required")
            .Length(5, 50).WithMessage("PaidiDto SkiniName must be between 2 and 50 characters");

        RuleFor(user => user.PaidiType)
            .NotNull().WithMessage("PaidiType is required");

        When(paidi => paidi.PaidiType == PaidiType.Kataskinotis,()=>
        {
            RuleFor(paidi => paidi.Age)
                .GreaterThan(6).WithMessage("Kataskinotis must be older than 6 years old")
                .LessThan(16).WithMessage("Kataskinotis must be younger than 16 years old");
        });

        When(paidi => paidi.PaidiType == PaidiType.Ekpaideuomenos, () =>
        {
            RuleFor(paidi => paidi.Age)
                .NotEqual(6).WithMessage("Ekpaideuomenos must be 16 years old");
        });
    }
}

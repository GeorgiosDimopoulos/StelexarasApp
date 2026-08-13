using FluentValidation;

namespace StelexarasApp.Services.Validators;

public class PaidiValidator : AbstractValidator<PaidiDtoBase>
{
    public PaidiValidator()
    {
        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("PaidiDto last Name is required")
            .Length(3, 50).WithMessage("PaidiDto Name must be between 3 and 50 characters");
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("PaidiDto first Name is required")
            .Length(3, 50).WithMessage("PaidiDto Name must be between 3 and 50 characters");

        RuleFor(user => user.SkiniName)
            .NotEmpty().WithMessage("PaidiDto SkiniName is required")
            .Length(3, 50).WithMessage("PaidiDto SkiniName must be between 3 and 50 characters");

        RuleFor(user => user.PaidiType)
            .NotNull().WithMessage("PaidiType is required");
        RuleFor(paidi => paidi.Age)
                .NotEqual(16).WithMessage("Ekpaideuomenos must be 16 years old");

        When(paidi => paidi.PaidiType == PaidiType.Kataskinotis, () =>
        {
            RuleFor(paidi => paidi.Age)
                .NotEqual(16).WithMessage("Kataskinotis must not be 16 years old");
        });
        When(paidi => paidi.PaidiType == PaidiType.Ekpaideuomenos, () =>
        {
            RuleFor(paidi => paidi.Age)
                .Equal(16).WithMessage("Ekpaideuomenos must be 16 years old");
        });
    }
}
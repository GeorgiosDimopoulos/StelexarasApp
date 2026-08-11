using FluentValidation;

namespace StelexarasApp.Services.Validators;

public class StelexosValidator : AbstractValidator<StelexosDtoBase>
{
    public StelexosValidator()
    {
        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("StelexosName is required")
            .Length(2, 50).WithMessage("Stelexos Name must be between 2 and 50 characters");
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("StelexosName is required")
            .Length(2, 50).WithMessage("Stelexos Name must be between 2 and 50 characters");
        RuleFor(user => user.XwrosName)
            .NotEmpty().WithMessage("Stelexos XwrosName is required")
            .Length(2, 50).WithMessage("Stelexos XwrosName must be between 2 and 50 characters");
    }
}

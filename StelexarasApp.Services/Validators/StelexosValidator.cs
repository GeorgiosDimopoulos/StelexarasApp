using FluentValidation;
using StelexarasApp.Library.Dtos.Atoma;

namespace StelexarasApp.Services.Validators;

public class StelexosValidator : AbstractValidator<IStelexosDto>
{
    public StelexosValidator()
    {
        RuleFor(user => user.FullName)
            .NotEmpty().WithMessage("StelexosName is required")
            .Length(2, 50).WithMessage("Stelexos Name must be between 2 and 50 characters");
        
        RuleFor(user => user.XwrosName)
            .NotEmpty().WithMessage("Stelexos XwrosName is required")
            .Length(2, 50).WithMessage("Stelexos XwrosName must be between 2 and 50 characters");
        
        RuleFor(user => user.Thesi)
            .NotNull().WithMessage("Stelexos Thesi is required");

        RuleFor(user => user.Id)
            .InclusiveBetween(1, 999)
            .WithMessage("Id is required");
    }
}

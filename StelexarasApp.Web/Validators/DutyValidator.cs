using FluentValidation;
using StelexarasApp.Library.Models;

namespace StelexarasApp.Web.Validators;

public class DutyValidator : AbstractValidator<Duty>
{
    public DutyValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Duty Name is required")
            .Length(5, 50).WithMessage("Duty Name must be between 2 and 50 characters");
    }
}

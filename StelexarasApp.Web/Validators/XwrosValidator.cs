using FluentValidation;
using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.Web.Validators;

public class XwrosValidator : AbstractValidator<Xwros>
{
    public XwrosValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Xwros Name is required")
            .Length(5, 50).WithMessage("Xwros Name must be between 2 and 50 characters");
    }
}

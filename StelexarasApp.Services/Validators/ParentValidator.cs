using FluentValidation;
using StelexarasApp.Library.Dtos.People;

namespace StelexarasApp.Services.Validators;

public class ParentValidator : AbstractValidator<ParentDtoBase>
{
    public ParentValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("Parent first name is required")
            .Length(2, 50).WithMessage("Parent first name must be between 2 and 50 characters");

        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("Parent last name is required")
            .Length(2, 50).WithMessage("Parent last name must be between 2 and 50 characters");

        RuleFor(p => p.Phone)
            .NotEmpty().WithMessage("Parent phone is required")
            .Matches(@"^\+?[0-9 ]{10,15}$").WithMessage("Parent phone must be 10-15 digits");

        RuleFor(p => p.Sex)
            .IsInEnum();

        When(p => p.Age.HasValue, () =>
        {
            RuleFor(p => p.Age!.Value)
                .InclusiveBetween(18, 120)
                .WithMessage("Parent age must be between 18 and 120");
        });

        When(p => !string.IsNullOrWhiteSpace(p.Email), () =>
        {
            RuleFor(p => p.Email).EmailAddress();
        });
    }
}

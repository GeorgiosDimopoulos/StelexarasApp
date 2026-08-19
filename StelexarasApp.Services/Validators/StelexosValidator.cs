using FluentValidation;

namespace StelexarasApp.Services.Validators;

public class StelexosValidator : AbstractValidator<StelexosDtoBase>
{
    public StelexosValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(x => x.XwrosName)
            .NotEmpty();

        RuleFor(x => x.Age)
            .GreaterThan(18);

        RuleFor(x => x.Sex)
            .IsInEnum();

        RuleFor(x => x.Thesi)
            .IsInEnum();
    }
}

public class CreateStelexosValidator : AbstractValidator<CreateStelexosRequest>
{
    public CreateStelexosValidator()
    {
        Include(new StelexosValidator());
    }
}

public class UpdateStelexosValidator : AbstractValidator<UpdateStelexosRequest>
{
    public UpdateStelexosValidator()
    {
        Include(new StelexosValidator());

        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}


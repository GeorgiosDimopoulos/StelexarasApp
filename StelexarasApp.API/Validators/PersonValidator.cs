using FluentValidation;

namespace StelexarasApp.API.Validators;

public class PersonValidator : AbstractValidator<CreateStelexosRequest>
{
    public PersonValidator()
    {
        RuleFor(request => request.Age)
            .GreaterThan(0);
        RuleFor(request => request.Sex)
            .IsInEnum();
        RuleFor(request => request.Thesi)
            .NotNull();
        RuleFor(request => request.FullName)
             .NotEmpty();
        RuleFor(request => request.XwrosName)
             .NotEmpty();
    }
}

public class UpdateStelexosValidator : AbstractValidator<UpdateStelexosRequest>
{
    public UpdateStelexosValidator()
    {
        RuleFor(request => request.Age)
            .GreaterThan(18);
        RuleFor(request => request.Thesi)
            .IsInEnum();
        RuleFor(request => request.Sex)
            .IsInEnum();
        RuleFor(request => request.FullName)
             .NotEmpty()
             .NotNull();
        RuleFor(request => request.XwrosName)
             .NotEmpty();
    }
}

public class CreatePaidiValidator : AbstractValidator<CreatePaidiRequest>
{
    public CreatePaidiValidator()
    {
        RuleFor(request => request.PaidiType)
             .IsInEnum();
        RuleFor(request => request.Age)
            .GreaterThan(0)
            .LessThan(17);
        RuleFor(request => request.Sex)
            .IsInEnum();
        RuleFor(request => request.FullName)
             .NotEmpty()
             .NotNull();
        RuleFor(request => request.SkiniName)
             .NotEmpty();
    }
}

public class UpdatePaidiValidator : AbstractValidator<UpdatePaidiRequest>
{
    public UpdatePaidiValidator()
    {
        RuleFor(request => request.Age)
            .GreaterThan(0)
            .LessThan(17);
        RuleFor(request => request.Sex)
            .IsInEnum();
        RuleFor(request => request.FullName)
             .NotEmpty()
             .NotNull();
        RuleFor(request => request.SkiniName)
             .NotEmpty();
        RuleFor(request => request.PaidiType)
             .IsInEnum();
    }
}

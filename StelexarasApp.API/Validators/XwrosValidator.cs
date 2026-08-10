using FluentValidation;

namespace StelexarasApp.API.Validators;

public class CreateSkiniValidator : AbstractValidator<CreateSkiniRequest>
{
    public CreateSkiniValidator()
    {
        RuleFor(request => request.Sex)
             .IsInEnum();
        RuleFor(request => request.Name)
             .NotEmpty();
        RuleFor(request => request.KoinotitaId)
            .GreaterThan(0);
    }
}

public class UpdateSkiniValidator : AbstractValidator<UpdateSkiniRequest>
{
    public UpdateSkiniValidator()
    {
        RuleFor(request => request.Sex)
            .IsInEnum();
        RuleFor(request => request.Name)
             .NotEmpty();
        RuleFor(request => request.KoinotitaId)
             .GreaterThan(0);
    }
}


public class CreateKoinotitaValidator : AbstractValidator<CreateKoinotitaRequest>
{
    public CreateKoinotitaValidator()
    {
        RuleFor(request => request.Name)
             .NotEmpty();
        RuleFor(request => request.TomeasName)
             .NotEmpty();
    }
}

public class UpdateKoinotitaValidator : AbstractValidator<UpdateKoinotitaRequest>
{
    public UpdateKoinotitaValidator()
    {
        RuleFor(request => request.Name)
             .NotEmpty();
        RuleFor(request => request.TomeasName)
             .NotEmpty();
    }
}


public class CreateTomeasValidator : AbstractValidator<CreateTomeasRequest>
{
    public CreateTomeasValidator()
    {
        RuleFor(request => request.Name)
             .NotEmpty();
    }
}

public class UpdateTomeasValidator : AbstractValidator<UpdateTomeasRequest>
{
    public UpdateTomeasValidator()
    {
        RuleFor(request => request.Name)
             .NotEmpty();
    }
}
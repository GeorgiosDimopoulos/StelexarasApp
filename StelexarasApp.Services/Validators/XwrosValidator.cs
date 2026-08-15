using FluentValidation;

namespace StelexarasApp.Services.Validators;


public class CreateSkiniValidator : AbstractValidator<CreateSkiniRequest>
{
    public CreateSkiniValidator()
    {
        RuleFor(request => request.Sex)
             .IsInEnum();
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Xwros Name is required")
            .Length(5, 50).WithMessage("Xwros Name must be between 2 and 50 characters");
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
        RuleFor(request => request.KoinotitaId)
             .GreaterThan(0);
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Xwros Name is required")
            .Length(5, 50).WithMessage("Xwros Name must be between 2 and 50 characters");
    }
}


public class CreateKoinotitaValidator : AbstractValidator<CreateKoinotitaRequest>
{
    public CreateKoinotitaValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(5, 50).WithMessage("Xwros Name must be between 2 and 50 characters");        
        RuleFor(request => request.TomeasName)
             .NotEmpty();
    }
}

public class UpdateKoinotitaValidator : AbstractValidator<UpdateKoinotitaRequest>
{
    public UpdateKoinotitaValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(5, 50).WithMessage("Name must be between 2 and 50 characters");
        RuleFor(request => request.TomeasName)
             .NotEmpty();
    }
}


public class CreateTomeasValidator : AbstractValidator<CreateTomeasRequest>
{
    public CreateTomeasValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Xwros Name is required")
            .Length(5, 50).WithMessage("Xwros Name must be between 2 and 50 characters");
    }
}

public class UpdateTomeasValidator : AbstractValidator<UpdateTomeasRequest>
{
    public UpdateTomeasValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Xwros Name is required")
            .Length(5, 50).WithMessage("Xwros Name must be between 2 and 50 characters");
    }
}
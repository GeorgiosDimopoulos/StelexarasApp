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
            .IsInEnum()    
            .WithMessage("PaidiType is required");

        RuleFor(user => user.Sex)    
            .IsInEnum();

        When(paidi => paidi.PaidiType == PaidiType.Kataskinotis, () =>
        {
            RuleFor(paidi => paidi.Age)        
                .InclusiveBetween(6, 15)        
                .WithMessage("Kataskinotis age must be between 6 and 15");
        });
        When(paidi => paidi.PaidiType == PaidiType.Ekpaideuomenos, () =>
        {
            RuleFor(paidi => paidi.Age)
                .Equal(16);
        });
    }
}

public class CreatePaidiValidator : AbstractValidator<CreatePaidiRequest>
{
    public CreatePaidiValidator()
    {
        Include(new PaidiValidator());
    }
}

public class UpdatePaidiValidator : AbstractValidator<UpdatePaidiRequest>
{
    public UpdatePaidiValidator()
    {
        Include(new PaidiValidator());
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
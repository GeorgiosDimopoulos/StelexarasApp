using FluentValidation;
using StelexarasApp.Library.Models;

namespace StelexarasApp.Web.Validators;

public class ExpenseValidator : AbstractValidator<Expense>
{
    public ExpenseValidator()
    {
        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Expense Name is required")
            .Length(5, 50).WithMessage("Expense Name must be between 2 and 50 characters");

        RuleFor(e => e.Amount)
            .NotEmpty().WithMessage("Expense Amount is required");
    }
}

using Assignment.Application.DTOs.Supplier;
using FluentValidation;

namespace Assignment.Application.Validators;

public class ModifySupplierRequestValidator : AbstractValidator<ModifySupplierRequest>
{
    public ModifySupplierRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Supplier Name is required.");
        RuleFor(x => x.Name)
            .Length(3, 100)
            .WithMessage(
                "Supplier Name must be more than 3 characters and less than 100 characters."
            );
    }
}

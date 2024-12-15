using Assignment.Application.DTOs.Product;
using FluentValidation;

namespace Assignment.Application.Validators;

public class ModifyProductRequestValidator : AbstractValidator<ModifyProductRequest>
{
    public ModifyProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.");
        RuleFor(x => x.Name)
            .Length(3, 100)
            .WithMessage(
                "Product Name must be more than 3 characters and less than 100 characters."
            );

        RuleFor(x => x.QuantityPerUnit)
            .IsInEnum()
            .WithMessage("Product QuantityPerUnit is invalid.");

        RuleFor(x => x.ReorderLevel)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product ReorderLevel must be greater than or equal 0.");
        RuleFor(x => x.ReorderLevel)
            .LessThanOrEqualTo(int.MaxValue)
            .WithMessage($"Product ReorderLevel must be less than or equal {int.MaxValue}.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product UnitPrice must be greater than or equal 0.");
        RuleFor(x => x.UnitPrice)
            .LessThanOrEqualTo(decimal.MaxValue)
            .WithMessage($"Product UnitPrice must be less than or equal {decimal.MaxValue}.");

        RuleFor(x => x.UnitsInStock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product UnitsInStock must be greater than or equal 0.");
        RuleFor(x => x.UnitsInStock)
            .LessThanOrEqualTo(int.MaxValue)
            .WithMessage($"Product UnitsInStock must be less than or equal {int.MaxValue}.");
    }
}

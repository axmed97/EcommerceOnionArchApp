using Application.ViewModels.Products;
using FluentValidation;

namespace Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().NotNull().MinimumLength(5).MaximumLength(150);
            RuleFor(x => x.Quantity).NotEmpty().NotNull().Must(x => x >= 0);
            RuleFor(x => x.Price).NotEmpty().NotNull().Must(x => x >= 0);

        }
    }
}

using FluentValidation;

namespace Clean.Architecture.Business.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(50);

            RuleFor(v => v.Price).NotEqual(0);
        }
    }
}

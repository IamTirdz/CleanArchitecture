using FluentValidation;

namespace Clean.Architecture.Business.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();

            RuleFor(v => v.Price).NotEqual(0);
        }
    }
}

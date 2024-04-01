using FluentValidation;

namespace Clean.Architecture.Business.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.ProductId).NotEmpty();

            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();

            RuleFor(v => v.Price).NotEqual(0);
        }
    }
}

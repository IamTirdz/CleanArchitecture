using FluentValidation;

namespace Clean.Architecture.Business.Products.Commands.RemoveProduct
{
    public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
    {
        public RemoveProductCommandValidator()
        {
            RuleFor(v => v.ProductId).NotEmpty();
        }
    }
}

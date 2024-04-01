using FluentValidation;

namespace Clean.Architecture.Business.Products.Queries.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(v => v.ProductId).NotEmpty();
        }
    }
}

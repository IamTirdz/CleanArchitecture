using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Common.Models;
using Clean.Architecture.DataAccess.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Business.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
            if (product == null) throw new NotFoundException(new ErrorResponseDto { Message = "Product not found" });

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

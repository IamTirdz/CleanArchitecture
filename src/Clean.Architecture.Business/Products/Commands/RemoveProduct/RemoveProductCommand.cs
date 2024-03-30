using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Common.Models;
using Clean.Architecture.DataAccess.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Business.Products.Commands.RemoveProduct
{
    public class RemoveProductCommand : IRequest<bool>
    {
        public long ProductId { get; set; }
    }

    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, bool>
    {
        private readonly IAppicationDbContext _context;

        public RemoveProductCommandHandler(IAppicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
            if (product == null) throw new NotFoundException(new ErrorResponseDto { Message = "Product not found" });

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

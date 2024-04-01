using AutoMapper;
using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Common.Models;
using Clean.Architecture.Business.Products.Queries.GetProducts;
using Clean.Architecture.DataAccess.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Business.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public long ProductId { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
            if (product == null) throw new NotFoundException(new ErrorResponseDto { Message = "Product not found" });

            var result = _mapper.Map<ProductDto>(product);
            return result;
        }
    }
}

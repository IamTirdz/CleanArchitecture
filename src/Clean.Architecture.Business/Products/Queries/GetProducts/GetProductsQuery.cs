using AutoMapper;
using Clean.Architecture.DataAccess.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Business.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
            if (!products.Any()) return Enumerable.Empty<ProductDto>();

            var result = _mapper.Map<IEnumerable<ProductDto>>(products);
            return result;
        }
    }
}

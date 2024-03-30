using AutoMapper;
using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Common.Models;
using Clean.Architecture.DataAccess.DataContext;
using Clean.Architecture.DataAccess.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Business.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<long>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly IAppicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IAppicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);
            if (product != null) throw new BadRequestException(new ErrorResponseDto { Message = "Product already exist" });

            var newProduct = _mapper.Map<Product>(request);

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.Id;
        }
    }
}

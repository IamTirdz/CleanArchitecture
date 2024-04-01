using AutoMapper;
using Clean.Architecture.Business.Products.Queries.GetProducts;
using Clean.Architecture.DataAccess.DataContext;
using Clean.Architecture.DataAccess.Entities;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Clean.Architecture.Business.Tests.Products.Queries
{
    [TestFixture]
    public class GetProductsTests
    {
        Mock<IApplicationDbContext> _context;
        Mock<IMapper> _mapper;

        [SetUp]
        public void Initialize()
        {
            _context = new Mock<IApplicationDbContext>();
            _mapper = new Mock<IMapper>();
        }

        private GetProductsQueryHandler CreateHandler() => new(_context.Object, _mapper.Object);

        [Test]
        public async Task ShouldReturnListOfProductDto()
        {
            // Arrange
            var products = new List<Product>() { new() { Id = 123, Name = "Sample Product", Price = 10.0m } };
            var productDtos = new List<ProductDto>() { new() { Id = 123, Name = "Sample Product", Price = 10.0m } };
            var query = new GetProductsQuery();

            _context.Setup(c => c.Products).ReturnsDbSet(products);
            _mapper.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

            // Act
            var handler = CreateHandler();
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ProductDto>>();
            _context.Verify(v => v.Products, Times.Once());
            _mapper.Verify(v => v.Map<IEnumerable<ProductDto>>(It.IsAny<List<Product>>()), Times.Once);
        }

        [Test]
        public async Task ShouldReturnEmptyList()
        {
            // Arrange
            var products = new List<Product>();
            var query = new GetProductsQuery();

            _context.Setup(c => c.Products).ReturnsDbSet(products);

            // Act
            var handler = CreateHandler();
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
            _context.Verify(v => v.Products, Times.Once());
            _mapper.Verify(v => v.Map<ProductDto>(It.IsAny<Product>()), Times.Never);
        }
    }
}

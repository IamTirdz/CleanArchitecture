using AutoMapper;
using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Products.Queries.GetProductById;
using Clean.Architecture.Business.Products.Queries.GetProducts;
using Clean.Architecture.DataAccess.DataContext;
using Clean.Architecture.DataAccess.Entities;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Clean.Architecture.Business.Tests.Products.Queries
{
    public class GetProductByIdTests
    {
        Mock<IApplicationDbContext> _context;
        Mock<IMapper> _mapper;

        [SetUp]
        public void Initialize()
        {
            _context = new Mock<IApplicationDbContext>();
            _mapper = new Mock<IMapper>();
        }

        private GetProductByIdQueryHandler CreateHandler() => new(_context.Object, _mapper.Object);
        
        [Test]
        [TestCase(12, true)]
        [TestCase(34, true)]
        [TestCase(0, false)]
        public void ShouldReturnCorrectValidations(long? productId, bool expectedResult)
        {
            // Arrange
            var query = new GetProductByIdQuery { ProductId = productId!.Value };

            // Act
            var validator = new GetProductByIdQueryValidator();
            var result = validator.Validate(query);

            // Assert
            Assert.That(result.IsValid, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task ShouldReturnProductDto()
        {
            // Arrange
            var product = new Product { Id = 12, Name = "Sample Product", Price = 10.0m };
            var products = new List<Product>() { product };
            var productDto = new ProductDto { Id = 12, Name = "Sample Product", Price = 10.0m };            
            var query = new GetProductByIdQuery { ProductId = 12 };

            _context.Setup(c => c.Products).ReturnsDbSet(products);
            _mapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

            // Act
            var handler = CreateHandler();
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductDto>();
            _context.Verify(v => v.Products, Times.Once());
            _mapper.Verify(v => v.Map<ProductDto>(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public async Task ShouldThrowsException()
        {
            // Arrange
            var products = new List<Product>();
            var query = new GetProductByIdQuery { ProductId = 12 };

            _context.Setup(c => c.Products).ReturnsDbSet(products);
            
            // Act
            var handler = CreateHandler();
            var result = async() => await handler.Handle(query, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
            _context.Verify(v => v.Products, Times.Once());
            _mapper.Verify(v => v.Map<ProductDto>(It.IsAny<Product>()), Times.Never);
        }
    }
}

using AutoMapper;
using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Products.Commands.CreateProduct;
using Clean.Architecture.DataAccess.DataContext;
using Clean.Architecture.DataAccess.Entities;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Clean.Architecture.Business.Tests.Products.Commands
{
    public class CreateProductTests
    {
        Mock<IApplicationDbContext> _context;
        Mock<IMapper> _mapper;

        [SetUp]
        public void Initialize()
        {
            _context = new Mock<IApplicationDbContext>();
            _mapper = new Mock<IMapper>();
        }

        private CreateProductCommandHandler CreateHandler() => new(_context.Object, _mapper.Object);

        [Test]
        [TestCase("Sample Product", 100, true)]
        [TestCase("Sample Product", 0, false)]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", 100, false)]
        public void ShouldReturnCorrectValidations(string productName, decimal price, bool expectedResult)
        {
            // Arrange
            var command = new CreateProductCommand { Name = productName, Price = price };

            // Act
            var validator = new CreateProductCommandValidator();
            var result = validator.Validate(command);

            // Assert
            Assert.That(result.IsValid, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task ShouldReturnProductId()
        {
            // Arrange
            var product = new Product { Id = 12, Name = "Sample Product", Price = 10.0m };
            var products = new List<Product>() { product };
            var command = new CreateProductCommand { Name = "Test Product", Price = 20m };

            _context.Setup(c => c.Products).ReturnsDbSet(products);
            _mapper.Setup(m => m.Map<Product>(command)).Returns(product);

            // Act
            var handler = CreateHandler();
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBe(0);
            _context.Verify(v => v.Products, Times.Exactly(2));
            _context.Verify(v => v.Products.AddAsync(It.IsAny<Product>(), CancellationToken.None), Times.Once);
            _context.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Once());
            _mapper.Verify(v => v.Map<Product>(It.IsAny<CreateProductCommand>()), Times.Once);
        }

        [Test]
        public async Task ShouldThrowsException()
        {
            // Arrange
            var products = new List<Product>() { new() { Id = 12, Name = "Sample Product", Price = 10.0m } };
            var command = new CreateProductCommand { Name = "Sample Product", Price = 10m };

            _context.Setup(c => c.Products).ReturnsDbSet(products);

            // Act
            var handler = CreateHandler();
            var result = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<BadRequestException>();
            _context.Verify(v => v.Products, Times.Once());
            _mapper.Verify(v => v.Map<Product>(It.IsAny<Product>()), Times.Never);
        }
    }
}

using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Products.Commands.UpdateProduct;
using Clean.Architecture.DataAccess.DataContext;
using Clean.Architecture.DataAccess.Entities;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Clean.Architecture.Business.Tests.Products.Commands
{
    public class UpdateProductTests
    {
        Mock<IApplicationDbContext> _context;

        [SetUp]
        public void Initialize()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        private UpdateProductCommandHandler CreateHandler() => new(_context.Object);

        [Test]
        [TestCase(12, "Sample Product", 100, true)]
        [TestCase(34, "Sample Product", 0, false)]
        [TestCase(0, "Sample Product", 0, false)]
        [TestCase(56, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", 100, false)]
        public void ShouldReturnCorrectValidations(long? productId, string productName, decimal price, bool expectedResult)
        {
            // Arrange
            var command = new UpdateProductCommand { ProductId = productId!.Value, Name = productName, Price = price };

            // Act
            var validator = new UpdateProductCommandValidator();
            var result = validator.Validate(command);

            // Assert
            Assert.That(result.IsValid, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task ShouldReturnUpdatedIsTrue()
        {
            // Arrange
            var products = new List<Product>() { new() { Id = 12, Name = "Sample Product", Price = 10.0m } };
            var command = new UpdateProductCommand { ProductId = 12, Name = "Test Product", Price = 20m };

            _context.Setup(c => c.Products).ReturnsDbSet(products);

            // Act
            var handler = CreateHandler();
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _context.Verify(v => v.Products, Times.Exactly(2));
            _context.Verify(v => v.Products.Update(It.IsAny<Product>()), Times.Once());
            _context.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Once());
        }

        [Test]
        public async Task ShouldThrowsException()
        {
            // Arrange
            var products = new List<Product>() { new() { Id = 12, Name = "Sample Product", Price = 10.0m } };
            var command = new UpdateProductCommand { ProductId = 34, Name = "Test Product", Price = 20m };

            _context.Setup(c => c.Products).ReturnsDbSet(products);

            // Act
            var handler = CreateHandler();
            var result = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
            _context.Verify(v => v.Products, Times.Once());
        }
    }
}

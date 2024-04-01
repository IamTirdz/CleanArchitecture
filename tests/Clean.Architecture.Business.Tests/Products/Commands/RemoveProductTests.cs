using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Products.Commands.RemoveProduct;
using Clean.Architecture.DataAccess.DataContext;
using Clean.Architecture.DataAccess.Entities;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Clean.Architecture.Business.Tests.Products.Commands
{
    public class RemoveProductTests
    {
        Mock<IApplicationDbContext> _context;

        [SetUp]
        public void Initialize()
        {
            _context = new Mock<IApplicationDbContext>();        }

        private RemoveProductCommandHandler CreateHandler() => new(_context.Object);

        [Test]
        [TestCase(123, true)]
        [TestCase(0, false)]
        public void ShouldReturnCorrectValidations(long? productId, bool expectedResult)
        {
            // Arrange
            var command = new RemoveProductCommand { ProductId = productId!.Value };

            // Act
            var validator = new RemoveProductCommandValidator();
            var result = validator.Validate(command);

            // Assert
            Assert.That(result.IsValid, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task ShouldReturnRemovedIsTrue()
        {
            // Arrange
            var products = new List<Product>() { new() { Id = 12, Name = "Test Product", Price = 10.0m } };
            var command = new RemoveProductCommand { ProductId = 12 };

            _context.Setup(c => c.Products).ReturnsDbSet(products);

            // Act
            var handler = CreateHandler();
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _context.Verify(v => v.Products, Times.Exactly(2));
            _context.Verify(v => v.Products.Remove(It.IsAny<Product>()), Times.Once());
            _context.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Once());
        }

        [Test]
        public async Task ShouldThrowsException()
        {
            // Arrange
            var products = new List<Product>() { new() { Id = 12, Name = "Sample Product", Price = 10.0m } };
            var command = new RemoveProductCommand { ProductId = 34 };

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

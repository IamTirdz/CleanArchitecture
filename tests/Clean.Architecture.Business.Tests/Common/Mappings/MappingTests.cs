using AutoMapper;
using NUnit.Framework;
using Clean.Architecture.DataAccess.Entities;
using Clean.Architecture.Business.Products.Queries.GetProducts;
using Clean.Architecture.Business.Products.Commands.CreateProduct;
using System.Runtime.CompilerServices;
using Clean.Architecture.Business.Common.Mappings;

namespace Clean.Architecture.Business.Tests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfiles>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Product), typeof(ProductDto))]
    [TestCase(typeof(CreateProductCommand), typeof(Product))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);

        Assert.That(destination, Is.Not.Null);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        return RuntimeHelpers.GetUninitializedObject(type);
        //return FormatterServices.GetUninitializedObject(type);
    }
}

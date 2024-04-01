using AutoMapper;
using Clean.Architecture.Business.Products.Commands.CreateProduct;
using Clean.Architecture.Business.Products.Queries.GetProducts;
using Clean.Architecture.DataAccess.Entities;

namespace Clean.Architecture.Business.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductCommand, Product>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Created, o => o.Ignore())
            .ForMember(d => d.Modified, o => o.Ignore());
    }
}

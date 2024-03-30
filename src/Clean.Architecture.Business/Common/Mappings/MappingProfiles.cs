using AutoMapper;
using Clean.Architecture.Business.Products.Queries.GetProducts;
using Clean.Architecture.DataAccess.Entities;

namespace Clean.Architecture.Business.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>();
    }
}

using AutoMapper;
using Clean.Architecture.Common.Dtos;
using Clean.Architecture.DataAccess.Entities;

namespace Clean.Architecture.Business.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SampleEntity, SampleDto>();
    }
}

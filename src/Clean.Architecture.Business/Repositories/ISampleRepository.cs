using Clean.Architecture.DataAccess.Entities;

namespace Clean.Architecture.Business.Repositories;

public interface ISampleRepository : IBaseRepository<SampleEntity>
{
    Task<SampleEntity?> GetByNameAsync(string name, CancellationToken cancellationToken);
}

using Clean.Architecture.Business.Repositories;
using Clean.Architecture.DataAccess.Contexts;
using Clean.Architecture.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.DataAccess.Repositories;

public class SampleRepository : BaseRepository<SampleEntity>, ISampleRepository
{
    public SampleRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<SampleEntity?> GetByNameAsync(string name, CancellationToken cancellationToken)
        => await Entities.Where(e => e.Name == name).FirstOrDefaultAsync(cancellationToken);
}

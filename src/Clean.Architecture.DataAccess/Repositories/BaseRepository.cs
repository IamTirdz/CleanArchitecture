using Clean.Architecture.Business.Repositories;
using Clean.Architecture.DataAccess.Contexts;
using Clean.Architecture.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.DataAccess.Repositories;

public class BaseRepository<TEntity>
    : IBaseRepository<TEntity> where TEntity
    : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> Entities;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
        Entities = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Entities.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await Entities.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await Entities.AddAsync(entity, cancellationToken);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => await Entities.AddRangeAsync(entities, cancellationToken);

    public void Update(TEntity entity)
        => Entities.Update(entity);

    public async Task RemoveAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            Entities.Remove(entity);
        }
    }
}

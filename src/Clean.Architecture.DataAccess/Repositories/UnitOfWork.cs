using Clean.Architecture.Business.Repositories;
using Clean.Architecture.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Clean.Architecture.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
        => _context = context;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => await _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction started.");

        await _context.SaveChangesAsync(cancellationToken);

        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();

        _transaction = null!;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();

            _transaction = null!;
        }
    }

    public void Dispose()
        => _context.Dispose();
}

using Clean.Architecture.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.DataAccess.DataContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<SampleEntity> Samples { get; set; }
}


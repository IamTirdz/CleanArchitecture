using Clean.Architecture.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.DataAccess.Contexts;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<SampleEntity> Samples { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.SetTableName(entityType.DisplayName());

            var idProperty = entityType.FindProperty("Id");
            if (idProperty?.ClrType == typeof(Guid))
                modelBuilder.Entity(entityType.ClrType).HasIndex("Id").IsUnique();
        }
    }
}

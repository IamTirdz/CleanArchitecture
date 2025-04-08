using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Clean.Architecture.DataAccess.Entities;

namespace Clean.Architecture.DataAccess.Configurations;

public class SampleConfiguration : IEntityTypeConfiguration<SampleEntity>
{
    public void Configure(EntityTypeBuilder<SampleEntity> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Sample");

        builder.ToTable("Sample");

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Price).IsRequired();
    }
}

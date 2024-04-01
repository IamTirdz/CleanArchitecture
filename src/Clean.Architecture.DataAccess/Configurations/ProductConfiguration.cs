using Clean.Architecture.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK_Product");

            builder.ToTable("Product");

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Price).IsRequired();
        }
    }
}

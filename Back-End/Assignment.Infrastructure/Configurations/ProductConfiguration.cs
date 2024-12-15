using Assignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Infrastructure.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.QuantityPerUnit).HasConversion<int>();
        builder.Property(x => x.ReorderLevel).IsRequired();
        builder.Property(x => x.SupplierId).IsRequired();
        builder.Property(x => x.UnitPrice).IsRequired().HasPrecision(18, 2);
        builder.Property(x => x.UnitsInStock).IsRequired();
        builder.Property(x => x.UnitsOnOrder).IsRequired();

        builder.HasOne(p => p.Supplier);
    }
}

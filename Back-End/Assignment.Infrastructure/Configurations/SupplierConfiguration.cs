using Assignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Infrastructure.Configurations;

internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasMany(s => s.Products).WithOne(x => x.Supplier).HasForeignKey(x => x.SupplierId);
    }
}

using System.Reflection;
using Assignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Persistence;

public class AssignmentDbContext(DbContextOptions<AssignmentDbContext> options) : DbContext(options)
{
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

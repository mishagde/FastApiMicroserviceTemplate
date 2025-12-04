using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TemplateProject.Domain.Entities;

namespace TemplateProject.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // ---------------------------
    // DbSets
    // ---------------------------
    public DbSet<Order> Orders => Set<Order>();


    // ---------------------------
    // Model building
    // ---------------------------
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all IEntityTypeConfiguration<> from Assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

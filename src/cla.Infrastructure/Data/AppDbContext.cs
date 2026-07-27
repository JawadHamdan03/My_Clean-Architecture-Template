using cla.Application.Common;
using cla.Domain.Entities;
using cla.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace cla.Infrastructure.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options),IAppDbContext
{
    public DbSet<Product> Products{get;set;}
    public DbSet<User> Users{get;set;}
    public DbSet<RefreshToken> RefreshTokens{get;set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }

}
using cla.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace cla.Application.Common;


public interface IAppDbContext
{
    public DbSet<Product> Products { get; }
    public DbSet<User> Users { get; }
    public DbSet<RefreshToken> RefreshTokens{get;}

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);


}
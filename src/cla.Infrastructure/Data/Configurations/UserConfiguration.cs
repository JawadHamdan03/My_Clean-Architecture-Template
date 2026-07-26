using cla.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cla.Infrastructure.Data.Configurations;


public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x=>x.Name).IsUnique();
        builder.Property(x=>x.Name).IsRequired();
        builder.Property(x=>x.Password).IsRequired();

    }
}
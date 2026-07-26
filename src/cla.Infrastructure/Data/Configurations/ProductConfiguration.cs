using cla.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cla.Infrastructure.Data.Configurations;


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x=>x.Name).HasColumnType("VARCHAR").HasMaxLength(20);
        builder.Property(x=>x.Price).IsRequired(true);

    }
}
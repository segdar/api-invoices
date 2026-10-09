using ApiInvoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiInvoices.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("PRODUCT");
        builder.HasKey(product => product.ProductId);
        builder.Property(product => product.ProductId).HasColumnName("PRODUCTID").ValueGeneratedOnAdd();
        builder.Property(product => product.Description).HasColumnName("DESCRIPTION").HasMaxLength(250).IsUnicode(false).IsRequired();
    }
}

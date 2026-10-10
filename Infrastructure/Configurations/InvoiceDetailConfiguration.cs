using ApiInvoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiInvoices.Infrastructure.Configurations;

public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        builder.ToTable("INVOICEDETAIL");
        builder.HasKey(detail => detail.InvoiceDetailId);
        builder.Property(detail => detail.InvoiceDetailId).HasColumnName("INVOICEDETAILID").ValueGeneratedOnAdd();
        builder.Property(detail => detail.InvoiceHeaderId).HasColumnName("INVOICEHEADERID").IsRequired();
        builder.Property(detail => detail.ProductId).HasColumnName("PRODUCTID").IsRequired();
        builder.Property(detail => detail.Quantity).HasColumnName("QUANTITY").IsRequired();
        builder.Property(detail => detail.Price).HasColumnName("PRICE").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(detail => detail.Subtotal).HasColumnName("SUBTOTAL").HasColumnType("decimal(18,2)").IsRequired();
       
    }
}

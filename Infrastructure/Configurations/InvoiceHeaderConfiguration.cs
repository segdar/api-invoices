using ApiInvoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiInvoices.Infrastructure.Configurations;

public class InvoiceHeaderConfiguration : IEntityTypeConfiguration<InvoiceHeader>
{
    public void Configure(EntityTypeBuilder<InvoiceHeader> builder)
    {
        builder.ToTable("INVOICEHEADER");
        builder.HasKey(invoice => invoice.InvoiceHeaderId);
        builder.Property(invoice => invoice.InvoiceHeaderId).HasColumnName("INVOICEHEADERID").ValueGeneratedOnAdd();
        builder.Property(invoice => invoice.ClientId).HasColumnName("CLIENTID").IsRequired();
        builder.Property(invoice => invoice.InvoiceNumber).HasColumnName("INVOICENUMBER").HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(invoice => invoice.InvoiceDate).HasColumnName("INVOICEDATE").HasColumnType("datetime").IsRequired();
        builder.Property(invoice => invoice.Status).HasColumnName("STATUS").HasMaxLength(20).IsUnicode(false).IsRequired();
        builder.Property(invoice => invoice.TotalAmount).HasColumnName("TOTALAMOUNT").HasColumnType("decimal(18,2)").IsRequired();
        builder.HasIndex(invoice => invoice.InvoiceNumber).IsUnique();
        builder.HasOne(invoice => invoice.Client)
            .WithMany(client => client.InvoiceHeaders)
            .HasForeignKey(invoice => invoice.ClientId);
    }
}

using ApiInvoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiInvoices.Infrastructure.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("CLIENT");
        builder.HasKey(client => client.ClientId);
        builder.Property(client => client.ClientId).HasColumnName("CLIENTID").ValueGeneratedOnAdd();
        builder.Property(client => client.Name).HasColumnName("NAME").HasMaxLength(150).IsUnicode(false).IsRequired();
        builder.Property(client => client.LastName).HasColumnName("LASTNAME").HasMaxLength(150).IsUnicode(false).IsRequired();
        builder.Property(client => client.Address).HasColumnName("ADDRESS").HasMaxLength(250).IsUnicode(false).IsRequired();
    }
}

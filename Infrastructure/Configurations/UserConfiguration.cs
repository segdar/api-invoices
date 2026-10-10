using ApiInvoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiInvoices.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USER");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).HasColumnName("ID").ValueGeneratedOnAdd();
        builder.Property(user => user.Username).HasColumnName("USERNAME").HasMaxLength(100).IsRequired();
        builder.Property(user => user.Name).HasColumnName("NAME").HasMaxLength(150).IsRequired();
        builder.Property(user => user.Email).HasColumnName("EMAIL").HasMaxLength(150).IsRequired();
        builder.Property(user => user.PasswordHash).HasColumnName("PASSWORDHASH").HasMaxLength(255).IsRequired();
        builder.Property(user => user.Status).HasColumnName("STATUS").HasMaxLength(20).IsRequired();
        builder.HasIndex(user => user.Username).IsUnique();
           

       
    }
}

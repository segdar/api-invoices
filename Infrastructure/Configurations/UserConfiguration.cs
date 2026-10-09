using ApiInvoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiInvoices.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    // Generated with BCrypt.Net.BCrypt.HashPassword("Admin123!") for deterministic EF seed data.
    private const string SeededPasswordHash = "$2a$11$TAZ6f2YwaL7zeBm166w6Mujy6kKap6KK6nGm7qaVR9nSr7ylzJj.G";

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USER");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).HasColumnName("ID").ValueGeneratedOnAdd();
        builder.Property(user => user.Username).HasColumnName("USERNAME").HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(user => user.Name).HasColumnName("NAME").HasMaxLength(150).IsUnicode(false).IsRequired();
        builder.Property(user => user.Email).HasColumnName("EMAIL").HasMaxLength(150).IsUnicode(false).IsRequired();
        builder.Property(user => user.PasswordHash).HasColumnName("PASSWORDHASH").HasMaxLength(255).IsUnicode(false).IsRequired();
        builder.Property(user => user.Status).HasColumnName("STATUS").HasMaxLength(20).IsUnicode(false).IsRequired();
        builder.HasIndex(user => user.Username).IsUnique();

        builder.HasData(new User
        {
            Id = 1,
            Username = "admin",
            Name = "Test Admin",
            Email = "admin@example.com",
            PasswordHash = SeededPasswordHash,
            Status = "active"
        });
    }
}
